using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Zenject;

namespace BackwardsCap
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private Animator playerAnimator;
        [Inject] private Rigidbody2D playerRB;
        [Inject] private MapManager map;
        private float speed = 3f;

        [HideInInspector] public bool HasControl = true;

        [Inject] private TextBubble textBubble;
        
        private Vector2 movement;
        
        [Inject(Id = "Cursor")] private RectTransform cursor;
        private float cZ;

        [FormerlySerializedAs("HoldingRight")] [HideInInspector] public GrabbableObject Holding;
        
        void Awake()
        {
            cZ = cursor.transform.position.z;
            cursor.gameObject.SetActive(false);


        }

        void Update()
        {
            if (!HasControl)
            {
                playerRB.velocity = Vector2.zero;
                return;
            }
            
            Movement();
            
            MouseHandler();
        }

        void MouseHandler()
        {
            cursor.gameObject.SetActive(true);
            var wp = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            cursor.transform.position = Vector3Int.RoundToInt(new Vector3(wp.x,wp.y,cZ));
            if (Input.GetMouseButtonDown(0))
            {
                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero);

                if (Holding != null&&Holding.UseOnMap)
                {
                    if(hit.transform!=null)Holding.Use(hit.transform.GetComponent<GrabbableObject>());
                    if(Holding!=null) Holding.Use(wp);
                }
                else
                {
                    //first raycast under mouse to see if we are clicking on the map, or something collidable
                    
                    if (hit.transform != null)
                    {
                        if (hit.transform.CompareTag("Object"))
                        {
                            
                            var g=hit.transform.GetComponent<GrabbableObject>();
                            if (Holding != null && !Holding.UseOnMap)
                            {
                                Holding.Use(g);
                            }
                            else if (g.Pickup()) Holding = g;
                        }
                        else if (hit.transform.CompareTag("Vendor"))
                        {
                            hit.transform.GetComponent<LimbVendor>().Open();
                        }
                    }
                    else
                    {

                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                DropHolding();
            }
        }

        public void AddToHand(GrabbableObject o)
        {
                if (Holding != null)
                {
                    Holding.Drop(movement);
                }

                Holding = o;
                o.Pickup();
            
        }

        public void DropHolding(bool tellObject = true)
        {
            if (Holding != null)
            {
                if(tellObject)Holding.Drop(movement);
            }
            Holding = null;
        }

        
        void Movement()
        {
            movement = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                movement.y += 1f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                movement.y -= 1f;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                movement.x += 1f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                movement.x -= 1f;
            }
            
            playerAnimator.SetBool("Walking",movement.magnitude!=0f);

            playerRB.velocity = movement*speed;
            
        }
    }
}
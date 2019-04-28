using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace BackwardsCap
{
    public class GrabbableObject : MonoBehaviour
    {
        [Inject(Id = "Hand")] protected Transform hand;
        [Inject] protected MapManager map;
        [Inject] private Rigidbody2D player;
        [Inject] private LifeManager lifeManager;

        [SerializeField]protected ObjectModel model;
        
        public virtual void Pickup(Vector3 offset)
        {
            transform.parent = hand;
            transform.DOLocalMove(offset, 0.5f);
        }
        
        public virtual void Pickup()
        {
            transform.parent = hand;
            transform.DOLocalRotate(new Vector3(0, 0, 30f), 0.25f);
            transform.DOLocalMove(Vector3.zero, 0.25f);
        }

        public virtual void Drop()
        {
            transform.DOPause();
            StartCoroutine(Falling());
            transform.parent = null;
        }

        IEnumerator Falling()
        {
            float duration = 0.5f;
            float t = 0f;
            Vector2 velocity = (transform.position.xy()-player.position).normalized/20f;
            float rotMod = Random.Range(-1f, 1f);
            while (t < 1f)
            {
                t=t.AddClamped(Time.deltaTime / duration,0f,1f);
                transform.eulerAngles+=new Vector3(0f,0f,rotMod*velocity.x*100f*(1f-t));
                transform.position += velocity.xyz() * (1f - t);
                yield return 0f;
            }

            yield return 0f;
        }

        public virtual void Use(Vector3 wp)
        {
            
        }

        public virtual void LifeCost()
        {
            lifeManager.Subtract(model.LaborCost);
        }
    }
}
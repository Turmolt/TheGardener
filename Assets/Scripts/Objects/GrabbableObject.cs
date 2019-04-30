using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace BackwardsCap
{
    public class GrabbableObject : MonoBehaviour
    {
        public bool UseOnMap = true;
        [Inject(Id = "Hand")] protected Transform hand;
        [Inject] protected MapManager map;
        [Inject] protected Rigidbody2D playerRB;
        [Inject] protected LifeManager lifeManager;
        [Inject] protected SoundEffectManager sfx;

        [SerializeField]protected ObjectModel model;
        
        public virtual bool Pickup(Vector3 offset)
        {
            transform.parent = hand;
            transform.DOLocalMove(offset, 0.5f);
            transform.DOLocalRotate(new Vector3(0, 0, 0f), 0.25f);
            return true;
        }
        
        public virtual bool Pickup()
        {
            transform.parent = hand;
            transform.DOLocalRotate(new Vector3(0, 0, 30f), 0.25f);
            transform.DOLocalMove(Vector3.zero, 0.25f);
            return true;
        }

        public virtual void Drop(Vector2 movement)
        {
            transform.DOPause();
            StartCoroutine(Falling(movement));
            transform.parent = null;
        }

        IEnumerator Falling(Vector2 movement)
        {
            float duration = 0.5f;
            float t = 0f;
            Vector2 velocity = (movement+(transform.position.xy()-playerRB.position)).normalized/20f;
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
        
        public virtual void Use(GrabbableObject g)
        {
            
        }

        public virtual void LifeCost()
        {
            lifeManager.Subtract(model.LaborCost);
        }
    }
}
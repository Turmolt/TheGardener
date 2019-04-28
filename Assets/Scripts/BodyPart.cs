using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class BodyPart : GrabbableObject
    {
        protected bool isPlanted = false;
        [Inject] protected PlayerController player;
        [Inject] protected DayManager day;
        [SerializeField] private BodyPartModel model;

        protected float currentValue=1;
        
        void Start()
        {
            day.DayPassedEvent += Grow;
            
        }

        public void Grow()
        {
            if (!isPlanted) return;
            if (map.CheckPlant(transform.position))
            {
                currentValue++;
                Debug.Log(name+" grows");
            }
            else
            {
                Debug.Log(name+" wasn't watered!");
            }
        }
        
        public override void Pickup()
        {
            if (isPlanted) return;
            
            base.Pickup();
        }
        public void Plant(Vector3 wp)
        {
            transform.DOPause();
            isPlanted = true;
            wp = Vector3Int.RoundToInt(wp);
            transform.parent = null;
            transform.position = new Vector3(wp.x,wp.y,transform.position.z);
            transform.rotation = Quaternion.identity;
            
        }
    }
}
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
        public bool IsPlanted => isPlanted;
        [Inject] protected PlayerController player;
        [Inject] protected DayManager day;

        protected float currentValue=1;
        protected float daysPlanted = 0f;
        
        void Start()
        {
            day.DayPassedEvent += Grow;
            currentValue = model.AsBodyPart().StartValue;
        }

        public void Grow()
        {
            if (!isPlanted) return;
            if (map.CheckPlant(transform.position,true))
            {
                currentValue+=model.AsBodyPart().Growth;
                Debug.Log(name+" grows");
            }
            else
            {
                Debug.Log(name+" wasn't watered!");
            }
        }

        public void Harvest()
        {
            if (!isPlanted || daysPlanted < model.AsBodyPart().Days) return;
            
            
        }
        
        public override bool Pickup()
        {
            if (isPlanted) return false;
            
            return base.Pickup();
        }
        public void Plant(Vector3 wp)
        {
            player.DropHolding(false);
            transform.DOPause();
            isPlanted = true;
            wp = Vector3Int.RoundToInt(wp);
            transform.parent = null;
            transform.position = new Vector3(wp.x,wp.y,transform.position.z);
            transform.rotation = Quaternion.identity;
            
        }
    }
}
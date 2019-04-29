using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class BodyPart : GrabbableObject
    {
        
        public GameObject[] GrowingLimbs;
        protected bool isPlanted = false;
        public bool IsPlanted => isPlanted;
        [Inject] protected PlayerController player;
        [Inject] protected PartSpawner spawner;
        [Inject] protected DayManager day;

        protected float currentValue=1;
        protected float daysPlanted = 0f;
        
        void Start()
        {
            for (int i = 0; i < GrowingLimbs.Length; i++)
            {
                GrowingLimbs[i].SetActive(false);
            }
            day.DayPassedEvent += Grow;
            currentValue = model.AsBodyPart().StartValue;
        }

        void OnDisable()
        {
            day.DayPassedEvent -= Grow;
        }

        public virtual void Grow()
        {
            if (!isPlanted) return;
            if (map.CheckPlant(transform.position,true))
            {
                currentValue=currentValue.AddClamped(model.AsBodyPart().Growth,0f,model.AsBodyPart().Max);
               
            }
            else
            {
            }

            for (int i = 0; i < GrowingLimbs.Length; i++)
            {
                if (i >= currentValue-2) break;
                GrowingLimbs[i].SetActive(true);
            }

            daysPlanted++;
        }

        
        public virtual void Harvest()
        {
            

        }

        public override void Use(GrabbableObject g)
        {
            if (g.GetType() == typeof(Blender))
            {
                
                player.DropHolding(false);
                ((Blender) g).Use(this);
                Debug.Log("VVVRRRRRRRR");
            }
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
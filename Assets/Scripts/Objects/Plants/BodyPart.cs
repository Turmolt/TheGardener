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
        public ParticleSystem ReadyParticleSystem;
        protected bool isPlanted = false;
        public bool IsPlanted => isPlanted;
        [Inject] protected PlayerController player;
        [Inject] protected PartSpawner spawner;
        [Inject] protected DayManager day;

        protected float currentValue=1;
        protected float daysPlanted = 0f;

        public float Value => model.AsBodyPart().Value;
        
        void Start()
        {
        
            //left/right limbs randomly!
            transform.localScale = Random.Range(0, 10) % 2 == 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            for (int i = 0; i < GrowingLimbs.Length; i++)
            {
                GrowingLimbs[i].SetActive(false);
            }
            day.DayPassedEvent += Grow;
            currentValue = model.AsBodyPart().StartReturn;
            ReadyParticleSystem.gameObject.SetActive(false);
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
                if (i >= (int)currentValue-1) break;
                GrowingLimbs[i].SetActive(true);
            }

            daysPlanted++;

            if (daysPlanted >= model.AsBodyPart().Days)
            {
                CheckGrowth();
            }

        }

        private void CheckGrowth()
        {   
            ReadyParticleSystem.gameObject.SetActive(true);

            bool canStillGrow = currentValue < model.AsBodyPart().Max;

            var mainModule = ReadyParticleSystem.main;
            mainModule.startColor = canStillGrow ? new Color(0.4f,0.4f,0.4f) : Color.white;
            
            var emissionModule = ReadyParticleSystem.emission;
            emissionModule.rateOverTime = canStillGrow ? 2 : 8;

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
            sfx.PlayAudio(sfx.Plant);
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
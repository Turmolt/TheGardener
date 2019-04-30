using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class Limb : BodyPart
    {

        [SerializeField] private GameObject dismembered;
        [SerializeField] private GameObject planted;

        void Awake()
        {
            dismembered.SetActive(true);
            planted.SetActive(false);
        }



        public override void Use(Vector3 wp)
        {
            if (map.PlantLimb(wp))
            {
                Plant(wp);
                player.DropHolding(false);
            }
        }

        public void Plant(Vector3 wp)
        {
            
            dismembered.SetActive(false);
            planted.SetActive(true);
            
            base.Plant(wp);
        }

        public override void Harvest()
        {
            if (!isPlanted || daysPlanted < model.AsBodyPart().Days) return;
            sfx.PlayAudio(sfx.Cut);
            map.HarvestSpot(transform.position);
            spawner.SpawnParts(model.AsBodyPart().Part,(int)currentValue,transform.position,true);
            Destroy(this.gameObject);
        }
    }
}
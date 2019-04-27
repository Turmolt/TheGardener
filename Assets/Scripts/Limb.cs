using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class Limb : GrabbableObject
    {
        private bool isPlanted = false;
        [Inject] private PlayerController player;

        [SerializeField] private GameObject dismembered;
        [SerializeField] private GameObject planted;

        void Awake()
        {
            dismembered.SetActive(true);
            planted.SetActive(false);
        }

        public override void Pickup()
        {
            if (isPlanted) return;
            
            base.Pickup();
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
            transform.DOPause();
            wp = Vector3Int.RoundToInt(wp);
            dismembered.SetActive(false);
            planted.SetActive(true);
            transform.parent = null;
            transform.position = new Vector3(wp.x,wp.y,transform.position.z);
            transform.rotation = Quaternion.identity;
            
        }
    }
}
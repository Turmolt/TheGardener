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
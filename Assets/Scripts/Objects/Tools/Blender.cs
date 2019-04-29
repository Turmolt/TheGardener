using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackwardsCap
{
    public class Blender : GrabbableObject
    {
        private bool isPlaced = false;

        public override bool Pickup()
        {
            if (isPlaced) return false;
            return base.Pickup();
        }

        public override void Use(Vector3 wp)
        {
            if(!isPlaced) isPlaced = true;
            
        }

        public override void Use(GrabbableObject o)
        {
            if (!o.GetType().IsSubclassOf(typeof(BodyPart)) || ((BodyPart) o).IsPlanted) return;

            Debug.Log("BLENDDD");
            Destroy(o.gameObject);
            //TODO: Blend them into pieces!
        
            
        }
    }
}
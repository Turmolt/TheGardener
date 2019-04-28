using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class WateringCan : GrabbableObject
    {
        [Inject(Id = "Left Hand")] private Transform leftHand;
        [SerializeField] private Transform handle;

        public override bool Pickup()
        {
            hand = leftHand;
            
            return base.Pickup(-handle.localPosition);
        }

        public override void Use(Vector3 wp)
        {
            if(map.WaterArea(wp)) LifeCost();
        }
    }
}
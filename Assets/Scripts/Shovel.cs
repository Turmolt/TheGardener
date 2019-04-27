using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackwardsCap
{
    public class Shovel : GrabbableObject
    {
        public override void Use(Vector3 wp)
        {
            if(map.DigHole(wp)) LifeCost();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class Shovel : GrabbableObject
    {
        
        public override void Use(Vector3 wp)
        {
            if (map.DigHole(wp))
            {
                sfx.PlayAudio(sfx.Dig);
                LifeCost();
            }
        }
    }
}
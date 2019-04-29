using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace BackwardsCap{
    public class Saw : GrabbableObject
    {
        public Transform handle;
        public override void Use(GrabbableObject bp)
        {
            bp.AsBodyPart().Harvest();
        }

        public override bool Pickup()
        {

            return Pickup(-handle.localPosition);

        }
    }
}
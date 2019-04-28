using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace BackwardsCap{
    public class Saw : GrabbableObject
    {
        public Transform handle;
        public override void Use(BodyPart bp)
        {
            bp.Harvest();
        }

        public override bool Pickup()
        {

            return Pickup(-handle.localPosition);

        }
    }
}
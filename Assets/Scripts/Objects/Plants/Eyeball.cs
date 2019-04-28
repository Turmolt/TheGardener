using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace BackwardsCap
{
    public class Eyeball : BodyPart
    {
        [Inject] private PlayerController player;
        public override void Use(Vector3 wp)
        {

            if (map.PlantEye(wp))
            {
                Debug.Log("Planting");
                Plant(wp);
                player.DropHolding(false);
            }
        }
        
        
    }
}
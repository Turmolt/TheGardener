using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace BackwardsCap
{
    public class Eyeball : GrabbableObject
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
        
        public void Plant(Vector3 wp)
        {
            transform.DOPause();
            wp = Vector3Int.RoundToInt(wp);
            transform.parent = null;
            transform.position = new Vector3(wp.x,wp.y,transform.position.z);
            transform.rotation = Quaternion.identity;
            
        }
        
        
    }
}
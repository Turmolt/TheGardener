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
        public override void Use(Vector3 wp)
        {

            if (map.PlantEye(wp))
            {
                Debug.Log("Planting");
                Plant(wp);
                player.DropHolding(false);
            }
        }

        public override void Harvest()
        {
            if (!isPlanted || daysPlanted < model.AsBodyPart().Days) return;
            sfx.PlayAudio(sfx.Cut);
            map.HarvestSpot(transform.position);
            spawner.SpawnParts(model.AsBodyPart().Part,Mathf.FloorToInt(currentValue),transform.position,true);
            Destroy(this.gameObject);
        }
    }
}
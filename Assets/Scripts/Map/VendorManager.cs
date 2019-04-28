using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class VendorManager : MonoBehaviour
    {
        [Inject] private LifeManager lifeManager;

        [Inject] private PartSpawner spawner;

        private Dictionary<Part, float> PartCost = new Dictionary<Part, float>
        {
            [Part.Hand] = 3.0f,
            [Part.Foot] = 4.0f,
            [Part.Nose] = 2.0f,
            [Part.Eye] = 1.0f
        };

        [SerializeField] private Transform spawnPosition;



        public void BuyPart(Part part)
        {
            float cost = PartCost[part];
            if (lifeManager.CurrentLife > cost)
            {
                lifeManager.Subtract(cost);

                spawner.SpawnParts(part,1,spawnPosition.position,false);
            }
        }
    }

    public enum Part
    {
        Hand,
        Foot,
        Nose,
        Eye
    }

}
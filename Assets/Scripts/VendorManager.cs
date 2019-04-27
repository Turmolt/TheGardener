using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class VendorManager : MonoBehaviour
    {
        [Inject] private LifeManager lifeManager;
        [Inject] private DiContainer container;

        private Dictionary<Part, float> PartCost = new Dictionary<Part, float>
        {
            [Part.Hand] = 3.0f,
            [Part.Foot] = 4.0f,
            [Part.Nose] = 2.0f,
            [Part.Eye] = 1.0f
        };

        private Dictionary<Part, GameObject> PartPrefab;
        
        [SerializeField] private Transform spawnPosition;

        [SerializeField] private GameObject hand;
        
        [SerializeField] private GameObject foot;
        
        [SerializeField] private GameObject nose;
        
        [SerializeField] private GameObject eye;

        void Start()
        {
            PartPrefab = new Dictionary<Part, GameObject>
            {
                [Part.Hand] = hand,
                [Part.Foot] = foot,
                [Part.Nose] = nose,
                [Part.Eye] = eye
            };
        }


        public void BuyPart(Part part)
        {
            float cost = PartCost[part];
            if (lifeManager.CurrentLife > cost)
            {
                lifeManager.Subtract(cost);


                var lg = Instantiate(PartPrefab[part], spawnPosition.position, Quaternion.identity);
                var limb = lg.GetComponent<BodyPart>(); 
                container.Inject(limb);
            
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
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BackwardsCap
{
    public class VendorManager : MonoBehaviour
    {
        [Inject] private LifeManager lifeManager;

        [Inject] private PartSpawner spawner;

        [Inject] private PlayerController player;

        [Inject(Id = "Hand Cost")] private TextMeshProUGUI handCostLabel;
        [Inject(Id = "Foot Cost")] private TextMeshProUGUI footCostLabel;
        [Inject(Id = "Nose Cost")] private TextMeshProUGUI noseCostLabel;
        [Inject(Id = "Eye Cost")] private TextMeshProUGUI eyeCostLabel;

        
        [Inject(Id = "Hand Return")] private TextMeshProUGUI handReturnLabel;
        [Inject(Id = "Foot Return")] private TextMeshProUGUI footReturnLabel;
        [Inject(Id = "Nose Return")] private TextMeshProUGUI noseReturnLabel;
        [Inject(Id = "Eye Return")] private TextMeshProUGUI eyeReturnLabel;
        
        
        [Inject(Id = "Hand Day Label")] private TextMeshProUGUI handDaysLabel;
        [Inject(Id = "Foot Day Label")] private TextMeshProUGUI footDaysLabel;
        [Inject(Id = "Nose Day Label")] private TextMeshProUGUI noseDaysLabel;
        [Inject(Id = "Eye Day Label")] private TextMeshProUGUI eyeDaysLabel;



        
        
        
        
        [SerializeField] private BodyPartModel handModel;
        [SerializeField] private BodyPartModel footModel;
        [SerializeField] private BodyPartModel noseModel;
        [SerializeField] private BodyPartModel eyeModel;

        private Dictionary<Part, float> PartCost;

        [SerializeField] private Transform spawnPosition;

        private LimbVendor activeVendor;


        void Start()
        {
            PartCost= new Dictionary<Part, float>
            {
                [Part.Hand] = handModel.Value+1f,
                [Part.Foot] = footModel.Value+2f,
                [Part.Nose] = noseModel.Value+5f,
                [Part.Eye] = eyeModel.Value+10f
            };
            handCostLabel.text = PartCost[Part.Hand].ToString();
            footCostLabel.text = PartCost[Part.Foot].ToString();
            noseCostLabel.text = PartCost[Part.Nose].ToString();
            eyeCostLabel.text = PartCost[Part.Eye].ToString();
            
            handReturnLabel.text = handModel.Value.ToString();
            footReturnLabel.text = footModel.Value.ToString();
            noseReturnLabel.text = noseModel.Value.ToString();
            eyeReturnLabel.text = eyeModel.Value.ToString();
            
            handDaysLabel.text = $"{handModel.Days} Days";
            footDaysLabel.text = $"{footModel.Days} Days";
            noseDaysLabel.text = $"{noseModel.Days} Days";
            eyeDaysLabel.text =$"{eyeModel.Days} Days";
        }


        public void BuyPart(Part part)
        {
            if (!player.HasControl) return;
            
            float cost = PartCost[part];

            lifeManager.Subtract(cost);

            spawner.SpawnParts(part,1,spawnPosition.position,false);
        
        }

        public bool Open(LimbVendor l)
        {
            if (activeVendor!=null&&l != activeVendor)
            {
                activeVendor.Toggle(false);
            }else if (l == activeVendor)
            {
                return false;
            }

            activeVendor = l;
            l.Toggle(true);
            return true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)&&activeVendor!=null)
            {
                CheckClick();
            }
        }
        
        private void CheckClick()
        {

            if (!activeVendor.MouseInsideMe)
            {
                Debug.Log(activeVendor.name);
                activeVendor.Toggle(false);
                activeVendor = null;
                
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
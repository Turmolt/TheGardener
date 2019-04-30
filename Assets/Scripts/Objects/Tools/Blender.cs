using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BackwardsCap
{
    public class Blender : GrabbableObject
    {
        private bool isPlaced = false;

        [Inject] private DayManager day;
        [Inject] private LifeManager life;
        
        [SerializeField] private GameObject BlendLow;
        [SerializeField] private GameObject BlendMed;
        [SerializeField] private GameObject BlendFull;

        [Inject(Id = "Blender Gauge Current")] private TextMeshProUGUI blenderCurrentLabel;
        [Inject(Id = "Blender Gauge Max")] private TextMeshProUGUI blenderMaxLabel;
        [Inject(Id = "Blender Fill")] private Image blenderGauge;

        private float currentValue=0f;
        private float maxValue=50;

        private float blenderFillOffset = .47f;

        void Start()
        {
            RefreshBlenderFill();
            day.DayPassedEvent += DayEnd;
        }

        private void OnDisable()
        {
            day.DayPassedEvent -= DayEnd;
        }

        public override bool Pickup()
        {
            if (isPlaced) return false;
            return base.Pickup();
        }

        public void DayEnd()
        {
            life.Add(currentValue);
            currentValue = 0f;
            RefreshBlenderFill();
        }

        public override void Use(Vector3 wp)
        {
            if(!isPlaced) isPlaced = true;
            
        }

        public override void Use(GrabbableObject o)
        {
            if (!o.GetType().IsSubclassOf(typeof(BodyPart)) || ((BodyPart) o).IsPlanted||currentValue==maxValue) return;
            sfx.PlayAudio(sfx.Blend);
            currentValue = currentValue.AddClamped( o.AsBodyPart().Value,0f,maxValue);
            RefreshBlenderFill();            
            Destroy(o.gameObject);
        }

        void RefreshBlenderFill()
        {
            blenderMaxLabel.text = maxValue.ToString("0.0");
            blenderCurrentLabel.text = currentValue.ToString("0.0");
            var percent = currentValue / maxValue;
            blenderGauge.fillAmount = (percent/2f).AddClamped(blenderFillOffset,0f,1f);            
            if (percent == 0f)
            {
                BlendLow.SetActive(false);
                BlendMed.SetActive(false);
                BlendFull.SetActive(false);
            }else if (percent <= 0.40f)
            {
                BlendLow.SetActive(true);
                BlendMed.SetActive(false);
                BlendFull.SetActive(false);
            }else if (percent < 1f)
            {
                BlendLow.SetActive(false);
                BlendMed.SetActive(true);
                BlendFull.SetActive(false);
            }
            else
            {
                BlendLow.SetActive(false);
                BlendMed.SetActive(false);
                BlendFull.SetActive(true);
            
            }
        }
    }
}
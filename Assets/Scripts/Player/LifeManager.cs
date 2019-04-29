using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BackwardsCap{
    public class LifeManager : IInitializable
    {
        [Inject(Id = "Life")] private Image lifeGauge;
        [Inject(Id = "Life Gauge Current")] private TextMeshProUGUI currentLifeLabel;
        [Inject(Id = "Life Gauge Max")] private TextMeshProUGUI maxLifeLabel;


        private float maxLife=10f;
        private float currentLife;
        public float CurrentLife => currentLife;

        public void Initialize()
        {
            currentLife = maxLife;
            RefreshHealBar();
        }
        
        public void AddMaxLife(float value)
        {
            maxLife += value;
        }

        
        public void RefreshLife(float percent)
        {

            currentLife += maxLife * percent;
            currentLife = Mathf.Min(maxLife, currentLife);
            
            RefreshHealBar();
        }

        public void Subtract(float amount)
        {
            currentLife -= amount;
            
            RefreshHealBar();
            
        }

        void RefreshHealBar()
        {
            lifeGauge.fillAmount = currentLife/maxLife;
            currentLifeLabel.text = currentLife.ToString("0.0");
            maxLifeLabel.text = maxLife.ToString("0.0");
        }

        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BackwardsCap{
    public class LifeManager : IInitializable
    {
        [Inject(Id = "Life")] private Image lifeGauge;

        private float maxLife=25f;
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
            percent /= 100f;

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
        }

        
    }
}
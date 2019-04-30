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
        [Inject] private EndText endText;
        [Inject] private PlayerController player;


        private float maxLife=10f;
        private float currentLife;
        public float CurrentLife => currentLife;

        public void Initialize()
        {
            currentLife = maxLife;
            RefreshHealBar();
        }

        public void Add(float value, bool overflow = true)
        {
            currentLife += value;
            if (currentLife > maxLife)
            {
                if (overflow)
                {
                    maxLife = currentLife;
                }
                else
                {
                    currentLife = maxLife;
                }
            }

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

            if (currentLife <= 0)
            {
                player.HasControl = false;
                endText.EndGame();
            }
            
            RefreshHealBar();
            
        }

        void RefreshHealBar()
        {
            if (currentLife < 0f) currentLife = 0f;
            lifeGauge.fillAmount = currentLife/maxLife;
            currentLifeLabel.text = currentLife.ToString("0.0");
            maxLifeLabel.text = maxLife.ToString("0.0");
        }

        
    }
}
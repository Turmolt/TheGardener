using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class Bed : MonoBehaviour
    {
        [Inject] private LifeManager lifeManager;
        [Inject] private DayManager dayManager;
        
        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                dayManager.DayEnd();
                lifeManager.RefreshLife(0.35f);
            }
        }
    }
}
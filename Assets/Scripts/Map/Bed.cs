using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class Bed : MonoBehaviour
    {
        [Inject] private DayManager dayManager;
        
        public void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Triggered!");
            if (col.CompareTag("Player"))
            {
                dayManager.DayEnd();
            }
        }
    }
}
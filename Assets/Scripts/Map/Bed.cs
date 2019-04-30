using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class Bed : MonoBehaviour
    {
        [Inject] private LifeManager lifeManager;
        [Inject] private DayManager dayManager;
        [Inject(Id = "Sleep")] private TextMeshProUGUI sleepText;
        [Inject(Id = "Night")] private CanvasGroup nightCG;
        [Inject] private PlayerController player;
        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                sleepText.text = "";


                GoToSleep();

                Invoke("WakeUp",2.5f);
            }
        }

        public void WakeUp()
        {
            
            nightCG.DOFade(0.0f, 1.0f).OnComplete(() => { sleepText.text = "";});
            player.HasControl = true;
        }

        void GoToSleep()
        {
            nightCG.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                StartCoroutine("Zzz...".FillText(sleepText,.2f)); 
                Night();
            });

            player.HasControl = false;
        }

        public void Night()
        {
            dayManager.DayEnd();
            lifeManager.RefreshLife(.25f);
        }

    }
}
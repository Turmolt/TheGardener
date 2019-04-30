using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class EndText : MonoBehaviour
    {
        public GameObject RestartButton;
        
        [Inject] private PlayerController player;
        private CanvasGroup cg;
        private TextMeshProUGUI text;
        private string endText = "YOU DIED";
        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            cg = GetComponent<CanvasGroup>();
        }

        public void EndGame()
        {
            player.HasControl = false;
            cg.alpha = 1f;
            StartCoroutine(endText.FillText(text));
            Invoke("EnableRestartButton",1.5f);
        }

        public void EnableRestartButton()
        {
            RestartButton.SetActive(true);
        }
    }
}
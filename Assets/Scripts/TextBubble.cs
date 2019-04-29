using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BackwardsCap
{
    public class TextBubble : MonoBehaviour
    {


        public CanvasGroup cg;

        private float characterDelay = 0.05f;
        
        public TextMeshProUGUI Text;

        private IEnumerator routine;


        public void StartText(string[] textToDisplay)
        {
            if (routine == null)
            {
                
                routine = DisplayText(textToDisplay);
                cg.DOFade(1.0f, 0.25f).OnComplete(() => { StartCoroutine(routine); });

            }
        }

        IEnumerator DisplayText(string[] textToDisplay)
        {
            string s = "";

            bool complete = false;

            int index = 0;

            while (!complete)
            {
                int i = 0;
                while (s != textToDisplay[index])
                {
                    s = textToDisplay[index].Substring(0, i++);
                    Text.text = s;
                    yield return new WaitForSeconds(0.05f);
                    
                    
                }

                while (true)
                {
                    if (Input.anyKey) break;
                    yield return new WaitForEndOfFrame();
                }

                if (index >= textToDisplay.Length-1)
                {
                    complete = true;
                }
                else
                {
                    index++;
                }

                yield return new WaitForEndOfFrame();
            }

            cg.DOFade(0f, 0.25f);
            routine = null;
            yield return 0;
        }
    }
}
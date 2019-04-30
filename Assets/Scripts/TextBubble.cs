using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BackwardsCap
{
    public class TextBubble : MonoBehaviour
    {


        [Inject] private CinemachineVirtualCamera camera;
        [Inject] private Rigidbody2D playerRB;
        public CanvasGroup cg;

        private float characterDelay = 0.05f;
        
        public TextMeshProUGUI Text;

        private IEnumerator routine;

        public TextMeshProUGUI Continue;

        public bool Complete => complete;
        bool complete = false;

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

            complete = false;

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
        
        public void StartText(TextCamera[] textToDisplay)
        {
            if (routine == null)
            {
              //  Continue.gameObject.SetActive(false);
                
                routine = DisplayText(textToDisplay);
                cg.DOFade(1.0f, 0.25f).OnComplete(() => { StartCoroutine(routine); });

            }
        }

        IEnumerator DisplayText(TextCamera[] textToDisplay)
        {
            string s = "";

            complete = false;

            int index = 0;
            var cam = textToDisplay[0].Cam;
            while (!complete)
            {
                if(cam!=null) cam.Priority = 11;
                int i = 0;
                yield return new WaitForSeconds(0.25f);
                while (s != textToDisplay[index].Text)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        s = textToDisplay[index].Text;
                        Text.text = s;
                        break;
                    }
                    s = textToDisplay[index].Text.Substring(0, i++);
                    if (s.EndsWith("<"))
                    {
                        var c = textToDisplay[index].Text[i] == '/' ? 3 : 2;
                        i += c;
                        s = textToDisplay[index].Text.Substring(0, i);
                    }
                    Text.text = s;
                    yield return new WaitForSeconds(0.005f);
                }

                yield return new WaitForSeconds(0.5f);
               // Continue.gameObject.SetActive(true);

                while (true)
                {
                    if (Input.GetKey(KeyCode.Space)) break;
                    yield return new WaitForEndOfFrame();
                }
               // Continue.gameObject.SetActive(false);
                if(cam!=null) cam.Priority = 1;
                if (index >= textToDisplay.Length-1)
                {
                    complete = true;
                }
                else
                {
                    
                    index++;
                    cam = textToDisplay[index].Cam;
                }

                yield return new WaitForEndOfFrame();
            }

            cg.DOFade(0f, 0.25f);
            if(cam!=null) cam.Priority = 1;
            routine = null;
            yield return 0;
        }
    }

    [Serializable]
    public class TextCamera
    {
        [TextArea]
        public string Text;

        public CinemachineVirtualCamera Cam;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace BackwardsCap
{
    public class GameManager : MonoBehaviour
    {

        public TextCamera[] IntroText;
        
        [Inject] private TextBubble textBubble;
        [Inject] private PlayerController playerController;
        
        void Start()
        {
            StartCoroutine(StartGame());
        }

        IEnumerator StartGame()
        {
            playerController.HasControl = false;
            textBubble.StartText(IntroText);
            
            while(!textBubble.Complete)yield return new WaitForEndOfFrame();

            playerController.HasControl = true;
            yield return 0;
        }

        public void Restart()
        {
            SceneManager.LoadScene("Main");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace BackwardsCap
{
    public class LimbVendor : MonoBehaviour
    {
        
        public Part partType;

        private bool active = false;
        [SerializeField] private CanvasGroup cg;
        [SerializeField] private Button buyButton;
        
        [Inject] private VendorManager vendorManager;

        public bool MouseInsideMe => mouseInsideMe;
        private bool mouseInsideMe = false;
        
        void Start()
        {
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
            buyButton.onClick.AddListener(Buy);
        }

        public void Open()
        {
            vendorManager.Open(this);
        }

        
        public void MouseHover(bool hovering)
        {
            mouseInsideMe = hovering;

        }
        
        public void Toggle(bool endState)
        {
            active = endState;
            cg.DOFade(endState?1f:0f, 0.5f);
            cg.interactable = endState;
            cg.blocksRaycasts = endState;
        }
        
        public void Buy()
        {
            vendorManager.BuyPart(partType);
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BackwardsCap
{
    public class LimbVendor : MonoBehaviour
    {
        public Part partType;
        [Inject] private VendorManager vendorManager;
        
        public void Buy()
        {
            vendorManager.BuyPart(partType);
        }
    }
}
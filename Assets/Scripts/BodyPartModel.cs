using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BackwardsCap
{
    [CreateAssetMenu(menuName="Models/BodyPartModel",order=5)]
    public class BodyPartModel : ScriptableObject
    {
        public float LaborCost=1;
        public float Growth=1;
        public float Days = 2;
        public float Reward = 2;
    }
}
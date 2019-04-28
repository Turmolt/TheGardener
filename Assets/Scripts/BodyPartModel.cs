using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BackwardsCap
{
    [CreateAssetMenu(menuName="Models/Body Part Model",order=5)]
    public class BodyPartModel : ObjectModel
    {
        /// <summary>
        /// How much does it increase each growth period?
        /// </summary>
        public float Growth=1;
        
        /// <summary>
        /// How many days until its at max?
        /// </summary>
        public float Days = 2;
        
        /// <summary>
        /// Points returned per growth point
        /// </summary>
        public float Reward = 2;
    }
}
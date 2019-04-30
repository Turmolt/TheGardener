using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BackwardsCap
{
    [CreateAssetMenu(menuName="Models/Body Part Model",order=5)]
    public class BodyPartModel : ObjectModel
    {
        /// <summary>
        /// What type am I?
        /// </summary>
        public Part Part;
        /// <summary>
        /// What is it worth to start?
        /// </summary>
        public float StartReturn = 1;
    
        /// <summary>
        /// How much does it increase each growth period?
        /// </summary>
        public float Growth=1;
        
        /// <summary>
        /// How many days until its at max?
        /// </summary>
        public float Days = 2;
        
        /// <summary>
        /// Parts returned per growth point
        /// </summary>
        public int Max = 2;
        
        /// <summary>
        /// Parts returned per growth point
        /// </summary>
        public float Value = 2;
    }
}
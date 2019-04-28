using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackwardsCap
{
    [CreateAssetMenu(menuName="Models/Object Model",order=1)]
    public class ObjectModel : ScriptableObject
    {        
        /// <summary>
        /// Cost to plant
        /// </summary>
        public float LaborCost=1;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class DayManager : IInitializable,ITickable
    {

        public void Initialize()
        {
            Debug.Log("Init");
        }

        public void Tick()
        {

        }
    }
}
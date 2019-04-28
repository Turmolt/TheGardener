using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class DayManager : IInitializable,ITickable
    {

        public Action DayPassedEvent;

        public void Initialize()
        {

        }

        public void Tick()
        {

        }

        public void DayEnd()
        {
            DayPassedEvent?.Invoke();
        }
    }
}
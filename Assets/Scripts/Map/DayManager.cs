using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace BackwardsCap
{
    public class DayManager : IInitializable,ITickable
    {

        public Action DayPassedEvent;
        private int daysPassed = 0;
        [Inject(Id = "Day Counter")] private TextMeshProUGUI counter;
        public void Initialize()
        {

        }

        public void Tick()
        {

        }

        public void DayEnd()
        {
            DayPassedEvent?.Invoke();
            daysPassed++;
            counter.text = $"Day {daysPassed}";
        }
    }
}
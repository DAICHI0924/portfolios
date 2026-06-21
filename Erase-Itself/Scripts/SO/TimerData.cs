using System;
using UnityEngine;
using R3;
using System.Collections.Generic;

namespace Kakky
{
    [CreateAssetMenu(fileName = "TimerData", menuName = "Kakky/TimerData")]
    public class TimerData : ScriptableObject
    {
        [SerializeField, Header("")] public SerializableReactiveProperty<float> CurrentTime;

        public SerializableReactiveProperty<bool> OnTimerStopped;
    }
}

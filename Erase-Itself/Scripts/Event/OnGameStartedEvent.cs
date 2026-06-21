using System.Diagnostics;

namespace Kakky
{
    public struct OnGameStartedEvent
    {
        public OnGameStartedEvent(float limitTime, int currentStageIndex)
        {
            LimitTime = limitTime;
            CurrentStageIndex = currentStageIndex;
        }
        public float LimitTime;
        public int CurrentStageIndex;
    }
}
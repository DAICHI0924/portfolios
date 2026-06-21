using Project.Key.SO;

namespace Kakky
{
    public struct OnGameClearEvent
    {
        public StageDataBase StageData;
        public TimerData TimerData;
        public SumClearTimeData SumClearTimeData;
        public OnGameClearEvent(StageDataBase stageData, TimerData timerData, SumClearTimeData sumClearTimeData)
        {
            StageData = stageData;
            TimerData = timerData;
            SumClearTimeData = sumClearTimeData;
        }
    }
}

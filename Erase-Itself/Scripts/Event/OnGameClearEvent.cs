using Project.Key.SO;

namespace Kakky
{
    public struct OnGameClearEvent
    {
        public StageDataBase StageData;
        public TimerData TimerData;
        public SumClearTimeData SumClearTimeData;

        public PlayerParamData PlayerParamData;

        public OnGameClearEvent(StageDataBase stageData, TimerData timerData, SumClearTimeData sumClearTimeData, PlayerParamData playerParamData)
        {
            StageData = stageData;
            TimerData = timerData;
            SumClearTimeData = sumClearTimeData;
            PlayerParamData = playerParamData;
        }
    }
}

using Ko.InGame.GameClear;
using Project.Key.Camera;
using UnityEngine;
using unityroom.Api;

namespace Kakky
{
    public class GameClearEventListener : EventListenerBase
    {
        public override void Start()
        {
            GameEventBus.OnGameClear += HandleGameClear;
        }

        private async void HandleGameClear(OnGameClearEvent onGameClearEvent)
        {
            ServiceLocator.Resolve<ITimerService>().StopTimer();
            ServiceLocator.Resolve<ICameraService>().ChangeCameraToGameClear();
            await ServiceLocator.Resolve<GameClearService>().PlayGameClearAnimationAsync(onGameClearEvent.StageData.StageDataList[onGameClearEvent.StageData.CurrentStageIndex.Value].LimitTime.Value, onGameClearEvent.TimerData.CurrentTime.Value, onGameClearEvent.PlayerParamData.Strength.Value);

            // 累積クリア時間を更新
            int clearTime = (int)(onGameClearEvent.StageData.StageDataList[onGameClearEvent.StageData.CurrentStageIndex.Value].LimitTime.Value - onGameClearEvent.TimerData.CurrentTime.Value - 10f * onGameClearEvent.PlayerParamData.Strength.Value);
            onGameClearEvent.SumClearTimeData.AddClearTime(clearTime);

            // 全ステージクリア済みの場合、Thanks画面を表示する
            if (
                onGameClearEvent
                    .StageData
                    .StageDataList[onGameClearEvent.StageData.CurrentStageIndex.Value]
                    .SceneName
                == onGameClearEvent
                    .StageData
                    .StageDataList[onGameClearEvent.StageData.CurrentStageIndex.Value]
                    .NextStageSceneName
            )
            {
                ServiceLocator.Resolve<Project.Key.Thanks.IThankService>().ShowThanks();
                UnityroomApiClient.Instance.SendScore(1, onGameClearEvent.SumClearTimeData.SumClearTimer, ScoreboardWriteMode.HighScoreAsc);
            }
        }

        public override void OnDestroy()
        {
            GameEventBus.OnGameClear -= HandleGameClear;
        }
    }
}

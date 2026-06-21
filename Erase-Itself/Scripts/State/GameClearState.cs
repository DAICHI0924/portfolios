using System;
using Cysharp.Threading.Tasks;
using Key.Core;
using Project.Key.ScreenFade;
using R3;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kakky
{
    public class GameClearState : StateBase, IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        public override async void OnEnter(GameDataContext context)
        {
            base.OnEnter(context); //親クラスの関数を実行する
            GameEventBus.RaiseGameClear(new OnGameClearEvent(context.StageDataBase, context.TimerData, context.SumClearTimeData));
            context.StageDataBase.CurrentStageIndex.Skip(1).Subscribe(index =>
            {
                var nextStageSceneName = context.StageData.NextStageSceneName;
                var currentStageSceneName = context.StageData.SceneName;
                LoadNextStageSceneAsync(currentStageSceneName, nextStageSceneName).Forget();
            }).AddTo(_disposables);
            Debug.Log($"Current Stage Index: {context.StageDataBase.CurrentStageIndex.Value}");
        }

        private async UniTaskVoid LoadNextStageSceneAsync(Generated.SceneName currentStageSceneName, Generated.SceneName nextStageSceneName)
        {
            await FadeManager.Instance.FadeOutAsync(1f);
            await AddictiveSceneMananger.UnloadAddictiveScene(currentStageSceneName);
            await AddictiveSceneMananger.LoadAddictiveScene(nextStageSceneName);
            await FadeManager.Instance.FadeInAsync(1f);
        }
        public override void OnUpdate(GameDataContext context)
        {
            base.OnUpdate(context); //親クラスの関数を実行する
            //TODO: ゲーム進行時の処理
        }
        public override void OnExit(GameDataContext context)
        {
            base.OnExit(context); //親クラスの関数を実行する
            _disposables.Clear();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}

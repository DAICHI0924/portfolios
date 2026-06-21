using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Kakky
{
    public class GameStartState : StateBase, IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        public override void OnEnter(GameDataContext context)
        {
            base.OnEnter(context);
            RunAsync(context).Forget();
        }

        private async UniTaskVoid RunAsync(GameDataContext context)
        {
            await GameEventBus.RaiseGameStartAsync(new OnGameStartedEvent(context.StageData.LimitTime.Value, context.StageDataBase.CurrentStageIndex.Value));
            InGameStateMachine.Instance.ChangeState(InGameState.Playing);
        }

        public override void OnUpdate(GameDataContext context)
        {
            base.OnUpdate(context);
            //TODO: ゲーム進行時の処理
        }

        public override void OnExit(GameDataContext context)
        {
            base.OnExit(context);
            _disposables.Clear();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}

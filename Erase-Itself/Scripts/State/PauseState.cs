using System;
using R3;
using UnityEngine;

namespace Kakky
{
    public class PauseState : StateBase, IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        public override void OnEnter(GameDataContext context)
        {
            base.OnEnter(context); //親クラスの関数を実行する
            //TODO: ゲーム開始時の処理(Eventの発行など)
            context.PlayerParamData.Strength
            .Subscribe(health =>
            {
                Debug.Log($"Player Health: {health}");
                Kakky.InGameStateMachine.Instance.ChangeState(InGameState.Playing);
            })
            .AddTo(_disposables);
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

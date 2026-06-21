using System;
using R3;
using UnityEngine;

namespace Kakky
{
    public class GameOverState : StateBase, IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        public override void OnEnter(GameDataContext context)
        {
            base.OnEnter(context); //親クラスの関数を実行する
            GameEventBus.RaiseGameOver();
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

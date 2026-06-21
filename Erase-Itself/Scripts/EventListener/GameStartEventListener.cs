using Cysharp.Threading.Tasks;
using Project.Key.Player;
using UnityEngine;

namespace Kakky
{
    public class GameStartEventListener : EventListenerBase
    {
        public override void Start()
        {
            Debug.Log("GameStartEventListener started.");
            GameEventBus.OnGameStart += HandleGameStart;
        }

        private async UniTask HandleGameStart(OnGameStartedEvent onGameStartedEvent)
        {
            ServiceLocator.Resolve<IPlayerService>().DisableMovement();
            for (int i = 0;i < 10;i++)
                ServiceLocator.Resolve<Project.Key.Text.ITextContainerService>().GenerateText();
            await ServiceLocator.Resolve<IGameStartService>().PlayGameStartAnimationAsync();
            // TODO: GameEventBus の OnGameStart が limitTime を渡せるようになったら有効化する
            ServiceLocator.Resolve<ITimerService>().StartTimer(onGameStartedEvent.LimitTime);
        }

        public override void OnDestroy()
        {
            Debug.Log("GameStartEventListener destroyed.");
            GameEventBus.OnGameStart -= HandleGameStart;
        }
    }
}

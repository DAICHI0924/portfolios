using Ko.InGame.GameOver;
using Project.Key.Camera;
using UnityEngine;

namespace Kakky
{
    public class GameOverEventListener : EventListenerBase
    {
        public override void Start()
        {
            GameEventBus.OnGameOver += HandleGameOver;
        }

        private async void HandleGameOver()
        {
            ServiceLocator.Resolve<ITimerService>().StopTimer();
            ServiceLocator.Resolve<ICameraService>().ChangeCameraToGameOver();
            ServiceLocator.Resolve<Project.Key.Player.IPlayerService>().DisableMovement();
            ServiceLocator.Resolve<Project.Key.Player.IPlayerService>().Explode();
            await ServiceLocator
                .Resolve<Ko.InGame.GameOver.GameOverService>()
                .PlayGameOverAnimationAsync();
        }

        public override void OnDestroy()
        {
            GameEventBus.OnGameOver -= HandleGameOver;
        }
    }
}

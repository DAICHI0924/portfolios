using Ko.InGame.GameClear;
using Project.Key.Camera;
using Project.Key.Player;
using UnityEngine;

namespace Kakky
{
    [DefaultExecutionOrder(-100)]
    public class ServiceRegisterer : MonoBehaviour
    {
        [SerializeField] private GameStartService _gameStartService;
        [SerializeField] private PlayerService _playerService;
        [SerializeField] private TimerService _timerService;
        [SerializeField] private Ko.InGame.GameOver.GameOverService _gameOverService;
        [SerializeField] private GameClearService _gameClearService;
        [SerializeField] private CameraService _cameraService;
        [SerializeField] private Project.Key.Text.TextContainerService _textContainerService;

        private void Awake()
        {
            ServiceLocator.Register<IGameStartService>(_gameStartService);
            ServiceLocator.Register<IPlayerService>(_playerService);
            ServiceLocator.Register<ITimerService>(_timerService);
            ServiceLocator.Register<Ko.InGame.GameOver.GameOverService>(_gameOverService);
            ServiceLocator.Register<GameClearService>(_gameClearService);
            ServiceLocator.Register<ICameraService>(_cameraService);
            ServiceLocator.Register<Project.Key.Text.ITextContainerService>(_textContainerService);
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<IGameStartService>();
            ServiceLocator.Unregister<IPlayerService>();
            ServiceLocator.Unregister<ITimerService>();
            ServiceLocator.Unregister<Ko.InGame.GameOver.GameOverService>();
            ServiceLocator.Unregister<GameClearService>();
            ServiceLocator.Unregister<ICameraService>();
            ServiceLocator.Unregister<Project.Key.Text.ITextContainerService>();
        }
    }
}

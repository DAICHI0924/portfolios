using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Kakky
{
    public static class GameEventBus
    {
        public static event Func<OnGameStartedEvent, UniTask> OnGameStart;
        public static event Action OnGameOver;
        public static event Action<OnGameClearEvent> OnGameClear;
        public static event Action OnPause;
        public static event Action OnPlaying;

        public static async UniTask RaiseGameStartAsync(OnGameStartedEvent onGameStartedEvent)
        {
            var list = OnGameStart?.GetInvocationList();
            if (list == null || list.Length == 0) return;

            var tasks = new UniTask[list.Length];

            for (var i = 0; i < list.Length; i++)
            {
                tasks[i] = ((Func<OnGameStartedEvent, UniTask>)list[i])(onGameStartedEvent);
            }

            await UniTask.WhenAll(tasks);
        }

        public static void RaiseStartOver(OnGameStartedEvent onGameStartedEvent) => OnGameStart?.Invoke(onGameStartedEvent);
        public static void RaiseGameOver() => OnGameOver?.Invoke();
        public static void RaiseGameClear(OnGameClearEvent onGameClearEvent) => OnGameClear?.Invoke(onGameClearEvent);
        public static void RaisePause() => OnPause?.Invoke();
        public static void RaisePlaying() => OnPlaying?.Invoke();
    }
}

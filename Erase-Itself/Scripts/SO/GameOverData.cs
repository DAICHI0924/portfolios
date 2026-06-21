using R3;
using UnityEngine;

namespace Kakky
{
    [CreateAssetMenu(fileName = "GameOverData", menuName = "Kakky/GameOverData")]
    public class GameOverData : ScriptableObject
    {
        private readonly Subject<Unit> _onGameOver = new();
        public Observable<Unit> OnGameOver => _onGameOver;

        public void RaiseGameOver() => _onGameOver.OnNext(Unit.Default);
    }
}

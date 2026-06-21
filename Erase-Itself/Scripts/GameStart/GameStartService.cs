using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Kakky
{
    public class GameStartService : MonoBehaviour, IGameStartService
    {
        [SerializeField] private GameStartAnimator _gameStartAnimator;
        [SerializeField] private GameObject _inGameUI;
        [SerializeField] private GameDataContext _gameDataContext;

        public async UniTask PlayGameStartAnimationAsync()
        {
            _gameDataContext.PlayerParamData.Strength.Value = 1f;
            _gameDataContext.PlayerParamData.RemainAmount.Value = 1f;
            _inGameUI.SetActive(false);
            await _gameStartAnimator.PlayGameStartAnimation();
            _inGameUI.SetActive(true);
        }
    }
}

using Cysharp.Threading.Tasks;
using Key.Core;
using Project.Key.ScreenFade;
using UnityEngine;

namespace Kakky
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        private async UniTask PlayAnimationAsync()
        {
            _panel.SetActive(true);
            await FadeManager.Instance.FadeOutAsync(1f);
            await AddictiveSceneMananger.UnloadAddictiveScene(Generated.SceneName.Title);
            await AddictiveSceneMananger.LoadAddictiveScene(Generated.SceneName.Stage1);
            await FadeManager.Instance.FadeInAsync(1f);
        }
        public void OnStartButtonClick()
        {
            PlayAnimationAsync().Forget();
        }
    }
}

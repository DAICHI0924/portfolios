using UnityEngine;
using UnityEngine.UI;
using R3;
using TMPro;

namespace Kakky
{
    public class TimerPresenter : MonoBehaviour
    {
        [SerializeField] private TimerData _timerData;
        [SerializeField] private TimerView _view;

        void Awake()
        {
            _timerData.CurrentTime
            .Subscribe(currentTime =>
            {
                _view.SetCurrentTime(currentTime);
            })
            .RegisterTo(this.destroyCancellationToken);
        }
    }
}
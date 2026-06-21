using UnityEngine;
using UnityEngine.UI;
using R3;
using TMPro;
using Alchemy.Inspector;

namespace Kakky
{
    public class TimerService : MonoBehaviour, ITimerService
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        [SerializeField] private TimerData _timerData;

        [LabelText("タイマースタート"), Button]
        public void StartTimer(float limitTime)
        {
            _timerData.CurrentTime.Value = limitTime;
            _timerData.OnTimerStopped.Value = false; // ReactiveProperty は値が変化しないと通知されないため、再スタート時に明示的にリセットする
            Observable.EveryUpdate()
                .TakeWhile(_ => _timerData.CurrentTime.Value > 0f)
                .Subscribe(_ =>
                {
                    _timerData.CurrentTime.Value =
                        Mathf.Max(0f, _timerData.CurrentTime.Value - Time.deltaTime);

                    if (_timerData.CurrentTime.Value <= 0f)
                    {
                        _timerData.OnTimerStopped.Value = true;
                    }
                })
                .AddTo(_disposables);
        }

        [LabelText("タイマーストップ"), Button]
        public void StopTimer()
        {
            _disposables.Clear();
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}

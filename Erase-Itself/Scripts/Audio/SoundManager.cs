using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.UI;

namespace Kakky
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _BGMSlider;
        [SerializeField] private Slider _SESlider;

        void Start()
        {
            if (_BGMSlider != null)
            {
                _masterSlider.onValueChanged.AddListener(OnMasterSliderValueChanged);
                _BGMSlider.onValueChanged.AddListener(OnBGMSliderValueChanged);
                _SESlider.onValueChanged.AddListener(OnSESliderValueChanged);
            }
        }

        private void OnMasterSliderValueChanged(float value)
        {
            BGMManager.Instance.ChangeBaseVolume(value / 10 * _masterSlider.value);
            SEManager.Instance.ChangeBaseVolume(value / 10 * _masterSlider.value);
            SEManager.Instance.Play(SEPath.SYSTEM20);
        }

        private void OnBGMSliderValueChanged(float value)
        {
            BGMManager.Instance.ChangeBaseVolume(value / 10);
        }

        private void OnSESliderValueChanged(float value)
        {
            SEManager.Instance.ChangeBaseVolume(value / 10);
            SEManager.Instance.Play(SEPath.SYSTEM20);
        }

        void OnDestroy()
        {
            _masterSlider.onValueChanged.RemoveAllListeners();
            _BGMSlider.onValueChanged.RemoveAllListeners();
            _SESlider.onValueChanged.RemoveAllListeners();
        }
    }
}
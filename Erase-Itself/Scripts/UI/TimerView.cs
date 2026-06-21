using UnityEngine;
using UnityEngine.UI;
using R3;
using TMPro;
using UnityEngine.InputSystem;

namespace Kakky
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        public void SetCurrentTime(float currentTime)
        {
            _text.text = currentTime.ToString("F2");
        }
    }
}
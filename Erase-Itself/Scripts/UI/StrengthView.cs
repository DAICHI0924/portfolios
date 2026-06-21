using UnityEngine;
using UnityEngine.UI;
using R3;
using TMPro;
using UnityEngine.InputSystem;

namespace Kakky
{
    public class StrengthView : MonoBehaviour
    {
        [SerializeField] private Image _strengthGauge;
        public void ChangeScale(float scale)
        {
            _strengthGauge.fillAmount = scale;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using R3;
using TMPro;
using UnityEngine.InputSystem;

namespace Kakky
{
    public class RemainAmountView : MonoBehaviour
    {
        [SerializeField] private Image _remainAmountGauge;
        public void ChangeScale(float scale)
        {
            _remainAmountGauge.fillAmount = scale;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using R3;
using TMPro;

namespace Kakky
{
    public class RemainAmountPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerParamData _playerParamData;
        [SerializeField] private RemainAmountView _view;

        void Awake()
        {
            _playerParamData.RemainAmount
            .Subscribe(remainAmount =>
            {
                _view.ChangeScale(remainAmount);
            })
            .RegisterTo(this.destroyCancellationToken);
        }
    }
}
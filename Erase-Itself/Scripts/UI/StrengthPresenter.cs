using UnityEngine;
using UnityEngine.UI;
using R3;
using TMPro;

namespace Kakky
{
    public class StrengthPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerParamData _playerParamData;
        [SerializeField] private StrengthView _view;

        void Awake()
        {
            _playerParamData.Strength
            .Subscribe(strength =>
            {
                _view.ChangeScale(strength);
            })
            .RegisterTo(this.destroyCancellationToken);
        }
    }
}
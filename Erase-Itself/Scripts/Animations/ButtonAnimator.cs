using UnityEngine;
using UnityEngine.EventSystems;
using LitMotion;
using LitMotion.Extensions;
using Alchemy.Inspector;
using KanKikuchi.AudioManager;

namespace Kakky
{
    public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ButtonSEManager _buttonSEManager;
        private RectTransform _rect;
        private Vector2 _initialPosition;
        void Start()
        {
            _rect = GetComponent<RectTransform>();
            _initialPosition = _rect.anchoredPosition;
        }

        private void OnEnterPointer()
        {
            LMotion.Create(
                    _initialPosition,
                    _initialPosition + new Vector2(-70, 0),
                    0.1f)
                .WithEase(Ease.OutCubic)
                .BindToAnchoredPosition(_rect);

            _buttonSEManager.PlayHoveringButtonSE();
        }
        private void OnExitPointer()
        {
            LMotion.Create(
                    _initialPosition + new Vector2(-70, 0),
                    _initialPosition,
                    0.1f)
                .WithEase(Ease.OutCubic)
                .BindToAnchoredPosition(_rect);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnterPointer();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExitPointer();
        }
    }
}
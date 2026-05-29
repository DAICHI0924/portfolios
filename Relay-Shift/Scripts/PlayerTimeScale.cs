using UnityEngine;
using UnityEngine.Splines;

namespace Kakky
{
    public class PlayerTimeScale : MonoBehaviour, ISlowable
    {
        private SplineAnimate _splineAnimate;
        private Animator _animator;
        private float _currentPosition;
        private float _defaultSpeed;
        private float _defaultAnimSpeed = 1f;

        private void Awake()
        {
            _splineAnimate = GetComponent<SplineAnimate>();
            _animator = GetComponentInChildren<Animator>();
            _defaultSpeed = _splineAnimate.MaxSpeed;
            _defaultAnimSpeed = _animator.speed;
        }

        public void SetTimeScale(float scale)
        {
            _currentPosition = _splineAnimate.NormalizedTime;
            _splineAnimate.MaxSpeed = _defaultSpeed * scale;
            _splineAnimate.NormalizedTime = _currentPosition;
            _animator.speed = _defaultAnimSpeed * scale;
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

namespace Kakky
{
    public class LidAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        private Vector3 _initialPosition;

        public Vector3 SetInitialPosition()
        {
            _transform = transform;
            _initialPosition = _transform.position;
            return _initialPosition;
        }
    }
}
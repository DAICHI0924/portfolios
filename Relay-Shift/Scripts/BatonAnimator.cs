using UnityEngine;
using LitMotion;
using Cysharp.Threading.Tasks;
using Key;

namespace Kakky
{
    public class BatonAnimator : MonoBehaviour
    {
        [SerializeField] GameObject _batonObject;
        private Rigidbody _batonRb;
        [SerializeField] private float _forwardPower = 10f;
        [SerializeField] private float _upPower = 5f;
        [SerializeField] private float _torquePower = 10f;
        [SerializeField] private Key.SO.CurrentPlayerRuntimeSet _currentPlayerRuntimeSet;
        private bool _isAnimating = false;
        private float _angle;

        void Start()
        {
            _batonRb = _batonObject.GetComponent<Rigidbody>();
            _batonRb.useGravity = false;
        }

        void Update()
        {
            if (_isAnimating && _batonRb.transform.position.y < 0.5f)
            {
                OnDeadBatonAnimation();
            }
        }

        public void OnDead()
        {
            _batonObject.transform.parent = null;
            _batonRb.useGravity = true;
            _isAnimating = true;
            GameObject _playerObject = _currentPlayerRuntimeSet.CurrentPlayer;
            _angle = _playerObject.transform.eulerAngles.y;
            _batonRb.angularVelocity = Vector3.zero;
            _batonRb.transform.eulerAngles = new Vector3(85, _angle, 0);
            _batonRb.AddTorque(_playerObject.transform.forward * _torquePower, ForceMode.Impulse);
        }

        private void OnDeadBatonAnimation()
        {
            _batonRb.linearVelocity = Vector3.zero;
            _batonRb.angularVelocity = Vector3.zero;

            _batonRb.transform.eulerAngles = new Vector3(85, _angle, 0);
            Vector3 direction = Quaternion.Euler(0, _angle, 0) * Vector3.forward;
            Vector3 force = direction * _forwardPower + Vector3.up * _upPower;

            _batonRb.AddForce(force, ForceMode.Impulse);
            _batonRb.AddTorque(_batonRb.transform.right * _torquePower, ForceMode.Impulse);
            _isAnimating = false;
        }
    }
}

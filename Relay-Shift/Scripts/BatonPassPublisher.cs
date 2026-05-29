using UnityEngine;

namespace Kakky
{
    public class BatonPassPublisher : MonoBehaviour
    {
        [SerializeField]
        private Key.SO.BatonPassEvent _batonPassEvent;

        [SerializeField]
        private CameraManager _cameraManager;

        [SerializeField]
        private Key.SO.CurrentPlayerRuntimeSet _currentPlayerRuntimeSet;
        private bool _isPending = false;
        private const float PENDING_DURATION = 2f;
        private float _pendingTimer = 0f;

        private void Update()
        {
            if (!_isPending)
                return;

            _pendingTimer += Time.deltaTime;
            if (_pendingTimer >= PENDING_DURATION)
            {
                _isPending = false;
                _pendingTimer = 0f;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (_isPending)
                return;
            var hitPlayer =
                other.attachedRigidbody != null
                    ? other.attachedRigidbody.gameObject
                    : other.gameObject;

            if (hitPlayer != _currentPlayerRuntimeSet.CurrentPlayer.GetComponentInChildren<Saikoro.PlayerController>().gameObject)
                return;
            LoggerManager.Log($"BatonPassPublisher: Detected collision with {other.gameObject.tag}");
            if (other.CompareTag("BatonPassCollider"))
            {
                _isPending = true;
                _pendingTimer = 0f;
                _cameraManager.SwitchCameraToBaton(gameObject);
                _batonPassEvent.Raise();
            }
        }
    }
}

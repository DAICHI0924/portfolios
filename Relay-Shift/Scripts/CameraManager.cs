using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using System;
using Unity.VisualScripting;

namespace Kakky
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineSequencerCamera _sequenceCamera;
        [SerializeField] private CinemachineCamera _Camera1;
        [SerializeField] private CinemachineCamera _Camera2;
        [SerializeField] private CinemachineCamera _Camera3;
        [SerializeField] private Key.SO.BatonPassEvent _batonPassEvent;
        [SerializeField] private Key.SO.DeadEvent _deadEvent;
        [SerializeField] private GameObject _batonObject;
        [SerializeField] private float _waitTime = 3f;
        [SerializeField] private float _fadeOutTime = 2f;
        void Start()
        {
            // Initialize camera priorities if needed
            _sequenceCamera.Priority.Value = 3;
            _Camera1.Priority.Value = 2;
            _Camera2.Priority.Value = 1;
            _Camera3.Priority.Value = 0;

            _batonPassEvent.OnSuccessBatonPass += SwitchCameraToNextRunner;
        }

        void SwitchCameraToNextRunner(GameObject target)
        {
            _Camera1.Follow = target.transform;
            _Camera1.Priority.Value = 20;
            _Camera2.Priority.Value = 10;
        }

        public void SwitchCameraToResult()
        {
            _Camera1.Priority.Value = 10;
            _Camera2.Priority.Value = 20;
            _Camera3.Priority.Value = 30;
        }

        public void SwitchCameraToStatic()
        {
            SwitchCameraToBaton(_batonObject);
            Destroy(_Camera2.GetComponent<CinemachinePositionComposer>());
            Destroy(_Camera2.GetComponent<CinemachineRotationComposer>());
            _Camera2.AddComponent<CinemachineHardLookAt>();
        }

        public void SwitchCameraToBaton(GameObject target)
        {
            _Camera2.Follow = target.transform;
            _Camera1.Priority.Value = 10;
            _Camera2.Priority.Value = 20;
        }

        public void SkipStartCamera()
        {
            _sequenceCamera.Priority.Value = 0;
            _Camera1.Priority.Value = 30;
            _Camera2.Priority.Value = 10;
            LoggerManager.Log("CameraManager: Skip start camera - switched to Camera2 with priority 20");
        }

        private void OnDestroy()
        {
            _batonPassEvent.OnSuccessBatonPass -= SwitchCameraToNextRunner;
        }
    }
}

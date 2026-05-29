using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TNRD;

namespace Kakky
{
    public class TimeScaleManager : MonoBehaviour
    {
        [SerializeField] private float _originalScale = 1f;
        [SerializeField] private float _slowMotionScale = 0.01f;
        [SerializeField] private List<SerializableInterface<ISlowable>> _slowables = new();
        [SerializeField] private Key.SO.BatonPassEvent _batonPassEvent;

        private void Awake()
        {
            _batonPassEvent.OnBatonPass += ActivateSlowMotion;
            _batonPassEvent.OnSuccessBatonPass += DeactivateSlowMotion;
        }

        private void ActivateSlowMotion()
        {
            foreach (var slowable in _slowables)
            {
                Debug.Log($"Activating slow motion for {slowable}");
                slowable.Value.SetTimeScale(_slowMotionScale);
            }
        }

        private void DeactivateSlowMotion(GameObject target)
        {
            _slowables.RemoveAt(_slowables.Count - 1);
            LoggerManager.Log($"Deactivating slow motion for {target.name}");
            _slowables.Add(new SerializableInterface<ISlowable>(target.GetComponent<ISlowable>()));
            foreach (var slowable in _slowables)
            {
                slowable.Value.SetTimeScale(_originalScale);
            }
        }

        private void OnDestroy()
        {
            _batonPassEvent.OnBatonPass -= ActivateSlowMotion;
            _batonPassEvent.OnSuccessBatonPass -= DeactivateSlowMotion;
        }
    }
}


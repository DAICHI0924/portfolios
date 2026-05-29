using UnityEngine;
using UnityEngine.InputSystem;

namespace Kakky
{
    public class RunnerChanger : MonoBehaviour
    {
        [SerializeField] private Key.SO.BatonPassEvent _batonPassEvent;
        [SerializeField] private GameObject _batonObject;
        private GameObject _currentRunner;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _batonPassEvent.OnSuccessBatonPass += ChangeRunner;
        }

        private void ChangeRunner(GameObject newRunner)
        {
            _currentRunner = newRunner;
            Transform parentObject =
                _currentRunner.transform
                .Find("HumanDummy_M White").transform
                .Find("Rig").transform
                .Find("B-root").transform
                .Find("B-hips").transform
                .Find("B-spine").transform
                .Find("B-chest").transform
                .Find("B-shoulder.L").transform
                .Find("B-upperArm.L").transform
                .Find("B-forearm.L").transform
                .Find("B-hand.L").transform;

            if (parentObject == null)
            {
                Debug.LogError("Parent object not found for the baton. Check the hierarchy and names.");
                return;
            }

            _batonObject.transform.SetParent(parentObject);
            GameObject gameObject = parentObject.gameObject;
            _batonObject.transform.position = gameObject.transform.position;
            _batonObject.transform.rotation = gameObject.transform.rotation;
            _batonObject.transform.localPosition = new Vector3(0.0199999996f, 0.129999995f, 0.0500000007f);
            _batonObject.transform.localRotation = Quaternion.Euler(31.2356911f, 144.90918f, 18.0530396f);
        }

        void OnDestroy()
        {
            _batonPassEvent.OnSuccessBatonPass -= ChangeRunner;
        }
    }
}
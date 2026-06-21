using UnityEngine;
using LitMotion;
using System.Collections.Generic;
using Alchemy.Inspector;
using Unity.Cinemachine;
using Cysharp.Threading.Tasks;
using TMPro;
using LitMotion.Extensions;
using KanKikuchi.AudioManager;

namespace Kakky
{
    public class GameStartAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _eraser;
        [SerializeField] private Transform _lid;
        [SerializeField] private Transform _pencase;
        [SerializeField] private GameObject _cinemachineGO1;
        [SerializeField] private GameObject _cinemachineGO2;
        [SerializeField] private GameObject _text;
        private CinemachineCamera _cinemachine1;
        private CinemachineCamera _cinemachine2;
        private Transform _cinemachinePosition1;
        private Transform _cinemachinePosition2;
        private Vector2 _initialTextPosition;
        private RectTransform _rectTransform;
        private TextMeshProUGUI _textMeshProUGUI;


        void Awake()
        {
            _cinemachine1 = _cinemachineGO1.GetComponent<CinemachineCamera>();
            _cinemachine2 = _cinemachineGO2.GetComponent<CinemachineCamera>();
            _cinemachinePosition1 = _cinemachineGO1.transform;
            _cinemachinePosition2 = _cinemachineGO2.transform;
            _rectTransform = _text.GetComponent<RectTransform>();
            _textMeshProUGUI = _text.GetComponent<TextMeshProUGUI>();
            _textMeshProUGUI.text = "Ready?";
        }

        [LabelText("a"), Button]
        public async UniTask PlayGameStartAnimation()
        {
            _rectTransform.anchoredPosition = new Vector2(0, 1080);
            _initialTextPosition = _rectTransform.anchoredPosition;
            _cinemachine1.Priority = 10;
            _cinemachine2.Priority = 0;
            var eraserInitialPosition = _eraser.position;
            var eraserRigidbody = _eraser.GetComponent<Rigidbody>();
            // 非Kinematic+Interpolate中はFixedUpdateの補間がTransform直書きと競合し、低FPSでキャッチアップが増えると元位置にスナップバックするため、演出中だけKinematic化する
            eraserRigidbody.isKinematic = true;
            // _cinemachinePosition1.position = eraserInitialPosition + _eraser.up * 10 + _eraser.right * 7 + _eraser.forward * -8;
            _cinemachine1.Follow = null;
            // _cinemachinePosition1.rotation = Quaternion.LookRotation(_eraser.position - _cinemachinePosition1.position);
            // _cinemachinePosition1.localEulerAngles += new Vector3(-20f, 0f, 0f);
            _eraser.position += new Vector3(0f, 100f, 0f);
            _eraser.position += _eraser.right * -8f;
            _pencase.position = _eraser.position;
            _pencase.rotation = _eraser.rotation;
            var eraserCurrentPosition = _eraser.position;
            var pencaseCurrentPosition = _pencase.position;
            var sequence1 = LSequence.Create();
            var sequence2 = LSequence.Create();
            var sequence3 = LSequence.Create();

            sequence1.Append(
                LMotion.Create(
                    pencaseCurrentPosition,
                    pencaseCurrentPosition + Vector3.up * -100f,
                    1f
                )
                .WithEase(Ease.OutQuad)
                .Bind(x => _pencase.position = x)
            );

            sequence1.Join(
                LMotion.Create(
                    eraserCurrentPosition,
                    eraserCurrentPosition + Vector3.up * -100f,
                    1f
                )
                .WithEase(Ease.OutQuad)
                .Bind(x => _eraser.position = x)
            );

            sequence1.Append(
                LMotion.Create(
                    _initialTextPosition,
                    _initialTextPosition + new Vector2(0, -1080),
                    0.5f
                )
                .WithEase(Ease.OutQuad)
                .BindToAnchoredPosition(_rectTransform)
            );

            await sequence1.Run();
            var currentPosition = _eraser.position;
            var lidCurrentPosition = _lid.position;
            _eraser.Rotate(90f, 0f, 0f, Space.Self);


            sequence2.Append(
                LMotion.Create(
                    _lid.localEulerAngles,
                    _lid.localEulerAngles + new Vector3(0f, 180f, 0f),
                    0.5f
                )
                .WithEase(Ease.OutQuad)
                .Bind(x => _lid.localEulerAngles = x)
            );

            sequence2.Append(
                LMotion.Create(
                    currentPosition,
                    eraserInitialPosition,
                    1f
                )
                .WithEase(Ease.OutQuad)
                .Bind(x => _eraser.position = x)
            );

            var startRot = _eraser.localEulerAngles;

            sequence2.Join(
                LMotion.Create(
                    startRot,
                    startRot + new Vector3(-90f, 0f, -3600f),
                    1f
                )
                .WithEase(Ease.OutQuad)
                .Bind(x => _eraser.localEulerAngles = x)
            );

            sequence2.Append(
                LMotion.Create(
                    _lid.localEulerAngles + new Vector3(0f, 180f, 0f),
                    _lid.localEulerAngles,
                    0.5f
                )
                .WithEase(Ease.OutQuad)
                .Bind(x => _lid.localEulerAngles = x)
            );

            await sequence2.Run();
            eraserRigidbody.isKinematic = false;

            _textMeshProUGUI.text = "Start!!";
            SEManager.Instance.Play(SEPath.START);

            _cinemachine1.Priority = 0;
            _cinemachine2.Priority = 10;

            pencaseCurrentPosition = _pencase.position;
            sequence3.Append(
                LMotion.Create(
                    pencaseCurrentPosition,
                    pencaseCurrentPosition + Vector3.up * 100f,
                    1f
                )
                .WithEase(Ease.InQuad)
                .Bind(x => _pencase.position = x)
            );

            await sequence3.Run();
            _text.SetActive(false);
        }
    }
}

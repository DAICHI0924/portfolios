using System;
using System.Collections.Generic;
using Coffee.UIEffects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AudioManager.SE;

namespace Result
{
    public class Facade : MonoBehaviour, IFacade
    {
        [SerializeField]
        private RectTransform _downImage;
        [SerializeField]
        private TMP_Text _gameScoreText;
        [SerializeField]
        private TMP_Text _gameTotalText;
        [SerializeField]
        private CanvasGroup _highScoreCanvasGroup;
        [SerializeField]
        private CurrentStageData _currentStageData;
        private float _total = 0;


        private void Awake()
        {
            _highScoreCanvasGroup.alpha = 0;
        }
        Sequence _seq;

        public async UniTask ShowResultScreen(List<float> scores, bool isNewHighScore = false)
        {
            _seq = DOTween.Sequence();
            _seq.Append(_downImage.DOAnchorPosY(0, 1f).SetEase(Ease.OutBounce));
            await _seq.AsyncWaitForCompletion();

            SEManager.Instance.Play(SEName.DrumRoll);
            for (int i = 0; i < scores.Count; i++)
            {
                _gameScoreText.text = "+" + scores[i];
                _seq = DOTween.Sequence();
                _seq.Append(_gameScoreText.rectTransform.DOAnchorPosX(0, 0.2f).From(new Vector2(-2000, 0)).SetEase(Ease.OutExpo));
                _seq.Append(_gameScoreText.rectTransform.DOAnchorPosY(-100, 0.5f).From(new Vector2(0, 50)).SetEase(Ease.InExpo));
                await _seq.AsyncWaitForCompletion();

                _seq = DOTween.Sequence();
                _total += scores[i];
                _gameTotalText.text = ((int)_total).ToString("D4");
                _seq.Append(_gameScoreText.rectTransform.DOAnchorPosX(-2000, 0).From(new Vector2(-2000, 0)).SetEase(Ease.OutExpo));
                _seq.Append(_gameTotalText.rectTransform.DOScale(1, 0.2f).From(0.5f).SetEase(Ease.OutElastic));
                _seq.JoinCallback(() => SEManager.Instance.Play(SEName.ResultScoreUp));
                await _seq.AsyncWaitForCompletion();
            }

            SEManager.Instance.Stop(SEName.DrumRoll);
            _seq = DOTween.Sequence();
            _seq.Append(_gameScoreText.rectTransform.DOAnchorPosX(-2000, 0.5f).From(new Vector2(-2000, 0)).SetEase(Ease.OutExpo));
            _seq.Append(_gameTotalText.rectTransform.DOScale(0.5f, 0.1f).SetEase(Ease.OutExpo));
            _seq.AppendCallback(() => {
                SEManager.Instance.Stop(SEName.DrumRoll);
                SEManager.Instance.Play(SEName.Cymbals);
            });
            _seq.Append(_gameTotalText.rectTransform.DOScale(1.5f, 0.25f).SetEase(Ease.OutElastic));
            _seq.AppendInterval(0.5f);
            if (isNewHighScore)
            {
                // ハイスコア更新
                // _currentStageData.Data.HighScore = (int)_total;
                // Debug.Log($"New high score: {(int)_total}");
                ServiceLocator.Resolve<SO.StageDatabase>().SaveHighScore(_currentStageData.StageIndex, (int)_total);
                _seq.Append(_highScoreCanvasGroup.GetComponent<RectTransform>().DOScale(1, 0.5f).From(8).SetEase(Ease.OutBounce));
                _seq.Join(_highScoreCanvasGroup.DOFade(1, 0.5f));
                _seq.JoinCallback(() => SEManager.Instance.Play(SEName.GetHighScore));
            }
            await _seq.AsyncWaitForCompletion();
            //Debug.Log("Done");
        }
    }
}

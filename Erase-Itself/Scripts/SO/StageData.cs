using System;
using UnityEngine;
using R3;
using System.Collections.Generic;

namespace Kakky
{
    [CreateAssetMenu(fileName = "StageData", menuName = "Kakky/StageData")]
    public class StageData : ScriptableObject
    {
        [SerializeField, Header("")] public SerializableReactiveProperty<float> LimitTime;
        [SerializeField, Header("")] public SerializableReactiveProperty<List<TextData>> Texts;
        [SerializeField, Header("このステージデータのシーン")] private Generated.SceneName _sceneName;
        public Generated.SceneName SceneName => _sceneName;
        [SerializeField, Header("次のステージのシーン")] private Generated.SceneName _nextStageSceneName;
        public Generated.SceneName NextStageSceneName => _nextStageSceneName;
    }
}

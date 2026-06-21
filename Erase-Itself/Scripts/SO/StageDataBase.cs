using System;
using UnityEngine;
using R3;
using System.Collections.Generic;

namespace Kakky
{
    [CreateAssetMenu(fileName = "StageDataBase", menuName = "Kakky/StageDataBase")]
    public class StageDataBase : ScriptableObject
    {
        [SerializeField, Header("")] private List<StageData> _stageDataList;
        public IReadOnlyList<StageData> StageDataList => _stageDataList;
        [SerializeField, Header("")] public SerializableReactiveProperty<int> CurrentStageIndex;
    }
}

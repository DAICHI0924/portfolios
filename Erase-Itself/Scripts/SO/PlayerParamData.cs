using UnityEngine;
using R3;

namespace Kakky
{
    [CreateAssetMenu(fileName = "PlayerParamData", menuName = "Kakky/PlayerParamData")]
    public class PlayerParamData : ScriptableObject
    {
        [SerializeField, Header("強度の値(0~1)")] public SerializableReactiveProperty<float> Strength;
        [SerializeField, Header("残量の値(0~1)")] public SerializableReactiveProperty<float> RemainAmount;
    }
}
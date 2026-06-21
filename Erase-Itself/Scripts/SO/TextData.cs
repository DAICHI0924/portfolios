using UnityEngine;
using R3;

namespace Kakky
{
    [CreateAssetMenu(fileName = "TextData", menuName = "Kakky/TextData")]
    public class TextData : ScriptableObject
    {
        [SerializeField, Header("")] public SerializableReactiveProperty<string> Word;
        [SerializeField, Header("")] public SerializableReactiveProperty<int> StrokeCount;
    }
}

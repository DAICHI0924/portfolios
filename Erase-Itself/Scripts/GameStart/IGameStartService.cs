using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Kakky
{
    public interface IGameStartService
    {
        UniTask PlayGameStartAnimationAsync();
    }
}
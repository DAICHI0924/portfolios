using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Kakky
{
    public class MockGameStartService : IGameStartService
    {
        public async UniTask PlayGameStartAnimationAsync()
        {
            Debug.Log("GameStartService is called;");
        }
    }
}
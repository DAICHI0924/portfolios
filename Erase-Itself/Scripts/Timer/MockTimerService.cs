using UnityEngine;

namespace Kakky
{
    public class MockTimerService : ITimerService
    {
        public void StartTimer(float limitTime)
        {
            Debug.Log($"Mock: タイマー開始 limitTime={limitTime}");
        }

        public void StopTimer()
        {
            Debug.Log("Mock: タイマー停止");
        }
    }
}

using tomochin.Resource;
using UnityEngine;

/// <summary>
/// 全プレイヤーの動作状態（開始・停止）を管理するクラス
/// </summary>
public class PlayerStopManager : MonoBehaviour
{
    public bool isStarted;      // いずれかのプレイヤーが動き出したか
    public bool isStopped;      // 全プレイヤーが停止しているか
    private float _stopTime = 0; // 停止判定用のタイマー

    [SerializeField, Tooltip("停止とみなす速度の閾値")] private float StopBorderVelocity;
    [SerializeField, Tooltip("速度が閾値以下になってから停止確定までの時間")] private float StopBorderTime;
    [SerializeField] private RunTimeRigidBodySO _runTimeRigidBodySO;

    private void Start()
    {
        isStarted = false;
        isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 動き出しており、かつ全て停止したと判定されたら状態を更新
        if (IsStarted() && AreAllStopped())
        {
            isStarted = false;
        }
    }

    /// <summary>
    /// いずれかのプレイヤーが動いているかチェックする
    /// </summary>
    bool IsStarted()
    {
        foreach (var rb in _runTimeRigidBodySO.GetAllResources())
        {
            // わずかでも動いていれば開始状態とする
            if (rb.linearVelocity.magnitude > 0.01f)
            {
                isStarted = true;
            }
        }
        return isStarted;
    }

    /// <summary>
    /// プレイヤーが停止したか判定する
    /// </summary>
    /// <returns>停止していればtrue</returns>
    bool AreAllStopped()
    {
        foreach (var rb in _runTimeRigidBodySO.GetAllResources())
        {
            // 水平方向（X-Z平面）の速度のみを取得
            Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

            // 水平速度が閾値を超えている場合：動いているとみなす
            if (horizontalVelocity.magnitude > StopBorderVelocity)
            {
                isStopped = false;
                _stopTime = 0;
            }
            // 水平速度が閾値以下の場合：停止処理へ
            else if (horizontalVelocity.magnitude <= StopBorderVelocity)
            {
                // 水平方向の速度のみゼロにし、Y軸（落下速度）は保持
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                _stopTime += Time.deltaTime;
                rb.angularVelocity = Vector3.zero;
            }

            // 一定時間停止状態が続いたら完了とする
            if (_stopTime > StopBorderTime)
            {
                isStopped = true;
            }
        }
        return isStopped;
    }
}

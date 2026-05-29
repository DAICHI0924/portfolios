using UnityEngine;

public class PlayerRotateManager : MonoBehaviour
{
    [SerializeField] private float _rotateManager;
    [SerializeField] private RunTimeRigidBodySO _runTimeRigidBodySO;
    private float _speed;
    private int _rotateDirection;

    private void FixedUpdate()
    {
        SetRotate();
    }

    public void SetRotateDirection()
    {
        _rotateDirection = Random.Range(0, 2); // ランダムに回転方向を決定
    }

    /// <summary>
    /// 移動中のプレイヤーを回転させる
    /// </summary>
    private void SetRotate()
    {
        foreach (var rb in _runTimeRigidBodySO.GetAllResources())
        {
            // 現在の移動速度に応じて回転速度を変える
            _speed = rb.linearVelocity.magnitude;
            if (_rotateDirection == 0)
            {
                rb.transform.Rotate(0, _speed * _rotateManager, 0);
            }
            else
            {
                rb.transform.Rotate(0, -_speed * _rotateManager, 0);
            }
        }
    }
}

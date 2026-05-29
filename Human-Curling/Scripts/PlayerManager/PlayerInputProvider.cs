using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// プレイヤーへの物理的な力（移動・回転）の適用を担当するクラス
/// </summary>
public class PlayerInputProvider : MonoBehaviour
{
    [SerializeField, Tooltip("パワー倍率")] private float _powerManager;
    private float _properVelocity; // プレイヤー固有の速度係数
    private Rigidbody _rb;
    private PlayerBase _player;

    /// <summary>
    /// 計算されたベクトルに基づいてプレイヤーに力を加える
    /// </summary>
    /// <param name="vec">マウスドラッグによる入力ベクトル</param>
    /// <param name="gameObject">対象のプレイヤーオブジェクト</param>
    /// <param name="maximumpower">最大パワー</param>
    /// <param name="minimumpower">最小パワー</param>
    public void SetPower(Vector3 vec, GameObject gameObject, float maximumpower, float minimumpower)
    {
        _player = gameObject.GetComponent<PlayerBase>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _properVelocity = _player._speed;

        // 最大パワーを超えている場合
        if (maximumpower < vec.magnitude)
        {
            // 正規化して最大値で力を加える（ドラッグ方向の逆向きに発射）
            _rb.AddForce(maximumpower * _powerManager * -vec.normalized * _properVelocity, ForceMode.Impulse);
        }
        // 最小パワー以上の場合（かつ最大未満）
        else if (minimumpower < vec.magnitude)
        {
            // 入力ベクトルに応じて力を加える
            _rb.AddForce(-vec * _powerManager * _properVelocity, ForceMode.Impulse);
        }
    }
}

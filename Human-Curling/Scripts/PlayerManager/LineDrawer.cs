using UnityEngine;

/// <summary>
/// プレイヤーを引っ張る際の予測線（方向と強さ）を描画するクラス
/// </summary>
public class LineDrawer : MonoBehaviour
{
    [SerializeField, Tooltip("方向・長さの調整用係数")] private float _directionManager;
    private Rigidbody _rb;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// 現在の引っ張り力に応じてラインを描画する
    /// </summary>
    /// <param name="currentForce">マウス入力による現在のベクトル</param>
    /// <param name="gameObject">対象のプレイヤーオブジェクト</param>
    /// <param name="maximumpower">最大パワー</param>
    /// <param name="minimumpower">最小パワー</param>
    public void SetDirection(Vector3 currentForce, GameObject gameObject, float maximumpower, float minimumpower)
    {
        _rb = gameObject.GetComponent<Rigidbody>();

        // 最大パワーを超えている場合は最大値に制限
        if (maximumpower < currentForce.magnitude)
        {
            currentForce = currentForce.normalized * maximumpower * _directionManager;
        }
        // 最小パワー未満の場合はゼロにする
        else if (minimumpower > currentForce.magnitude)
        {
            currentForce = Vector3.zero;
        }
        // それ以外の場合は係数を掛けて調整
        else
        {
            currentForce *= _directionManager;
        }

        // 始点をプレイヤーの位置、終点を力の方向と逆（引っ張りアクションの表現）に設定
        _lineRenderer.SetPosition(0, _rb.position);
        _lineRenderer.SetPosition(1, _rb.position - currentForce);
    }

    /// <summary>
    /// ラインの表示をクリアする（始点と終点を同じにする）
    /// </summary>
    public void DeleteDirection()
    {
        if (_rb != null)
        {
            _lineRenderer.SetPosition(1, _rb.position);
        }
    }
}

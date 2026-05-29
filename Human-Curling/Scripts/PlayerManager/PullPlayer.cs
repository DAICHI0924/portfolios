using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// マウスのドラッグ操作でプレイヤーを引っ張って飛ばす機能を持つクラス
/// </summary>
public class PullPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("最小パワー")] private float _minimumpower;
    [SerializeField, Tooltip("最大パワー")] private float _maximumpower;

    private bool _isSet = false;        // マウスクリックの開始位置が設定されているか
    private bool _isStarted;            // プレイヤーが動き出したか
    private bool _isStopped;            // プレイヤーが停止しているか
    private bool _startCheck;           // 動き出したことを検知したフラグ
    private bool _stopCheck;            // 停止したことを検知したフラグ
    private int _rotate;                // 飛ばした時の回転方向(0か1)

    public bool canPull;                // 現在引っ張れる状態かどうか

    private GameObject _playerObject = null;    // 現在操作中のプレイヤーオブジェクト
    private GameObject _targetObject = null;    // 操作対象の保持用

    private PlayerStopManager _playerStopManager;
    private PlayerInputProvider _playerInputProvider;
    private PlayerRotateManager _playerRotateManager;
    private LineDrawer _lineDrawer;

    private Vector3 _pos_Down;          // マウスクリック時の位置
    private Vector3 _currentForce = Vector3.zero; // 現在の引っ張りベクトル

    void Start()
    {
        _playerStopManager = GetComponent<PlayerStopManager>();
        _playerInputProvider = GetComponent<PlayerInputProvider>();
        _playerRotateManager = GetComponent<PlayerRotateManager>();
        _lineDrawer = GetComponent<LineDrawer>();
    }

    void Update()
    {
        _isStarted = _playerStopManager.isStarted;
        _isStopped = _playerStopManager.isStopped;

        // 停止中で、かつ引っ張れる状態の場合
        if (_isStopped && canPull)
        {
            // マウスクリック開始：対象のプレイヤーを選択
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        _playerObject = hit.collider.gameObject;
                        SetMouseDownPosition();
                        if (_targetObject == null)
                        {
                            _targetObject = _playerObject;
                        }
                    }
                }
            }

            // プレイヤーを選択中の場合
            if (_playerObject != null && _playerObject == _targetObject)
            {
                // ドラッグ中：引っ張りベクトルを計算し、予測線を表示
                if (Input.GetMouseButton(0))
                {
                    _currentForce = (GetMousePosition() - _pos_Down);
                    _lineDrawer.SetDirection(_currentForce, _playerObject, _maximumpower, _minimumpower);
                }

                // マウスを離した：力を加えて飛ばす
                if (Input.GetMouseButtonUp(0))
                {
                    if (_isSet)
                    {
                        SetMouseUpPosition();
                    }
                    _lineDrawer.DeleteDirection();
                    _playerObject = null;
                }
            }
        }

        // 一連の動作（発射〜停止）が終わったら状態をリセット
        if (_playerObject == null && _targetObject != null && _startCheck && _stopCheck)
        {
            _targetObject = null;
            _startCheck = false;
            _stopCheck = false;
        }

        // Qキーでターゲットを強制リセット
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _targetObject = null;
        }

        // 動作状態のフラグ更新
        if (_isStarted && !_isStopped)
        {
            _startCheck = true;
        }

        if (_isStopped && _startCheck)
        {
            _stopCheck = true;
        }
    }

    /// <summary>
    /// マウス位置をXZ平面上のベクトルとして取得する
    /// </summary>
    private Vector3 GetMousePosition()
    {
        Vector3 position = Input.mousePosition;
        return new Vector3(position.x, 0, position.y);
    }

    /// <summary>
    /// マウスダウン時の初期設定
    /// </summary>
    private void SetMouseDownPosition()
    {
        _pos_Down = GetMousePosition();
        _isSet = true;
    }

    /// <summary>
    /// マウスアップ時に力を計算し、プレイヤーを発射する
    /// </summary>
    private void SetMouseUpPosition()
    {
        _playerRotateManager.SetRotateDirection();
        _playerInputProvider.SetPower(GetMousePosition() - _pos_Down, _playerObject, _maximumpower, _minimumpower);
        _isSet = false;
    }
}

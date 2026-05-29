using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackDoubleItem : CapturableItem, ICollectableWithPlayerDependency
{
    public PlayerBase PlayerBase { get => base._playerBase; }

    /// <summary>
    /// アイテムが取得されたときに呼ばれる処理
    /// </summary>
    public override void OnCollected()
    {
        _playerBase.SetAttack(_playerBase._Attack * 2); //public変数なのにアンダーバーついてる
        Destroy(gameObject);
    }
}

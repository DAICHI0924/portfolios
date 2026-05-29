using UnityEngine;

public class VectorTurnAroundItem : CapturableItem, ICollectableWithPlayerDependency
{
    public PlayerBase PlayerBase { get => base._playerBase; }
    
    public override void OnCollected()
    {
        Debug.Log("VectorTurnAroundItem 取得");
        PlayerBase.gameObject.GetComponent<Rigidbody>().linearVelocity *= -1;   //Component参照SOにできるよ
        Destroy(gameObject);
    }
}

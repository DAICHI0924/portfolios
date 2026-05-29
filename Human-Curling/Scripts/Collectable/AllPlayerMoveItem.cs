using UnityEngine;

public class AllPlayerMoveItem : CapturableItem, ICollectableWithPlayerDependency
{
    [SerializeField] private RunTimeRigidBodySO _runTimeRigidBodySO;
    public PlayerBase PlayerBase { get => base._playerBase; }
    public override void OnCollected()
    {
        Debug.Log("VectorTurnAroundItem 取得");
        foreach (var rb in _runTimeRigidBodySO.GetAllResources())
            rb.linearVelocity = PlayerBase.GetComponent<Rigidbody>().linearVelocity;
        Destroy(gameObject);
    }
}


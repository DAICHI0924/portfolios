using UnityEngine;

public interface ICollectableWithPlayerDependency
{
    public PlayerBase PlayerBase { get; }
    public void OnCollected()
    {

    }
}

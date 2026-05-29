using UnityEngine;
using R3;
public class ScoreUpItem : CapturableItem, ICollectable
{
    public ItemType itemType => ItemType.PointUp;
    // private Subject<ItemType> onCollectedSubject = new Subject<ItemType>();
    // public Observable<ItemType> OnCollectedAsObservable => onCollectedSubject;
    [SerializeField] private ScoreUpEventBus scoreUpEventBus; //スコア加算イベントバス
    public int score; //加算量アイテム

    /// <summary>
    /// アイテムが取得されたときに呼ばれる処理
    /// </summary>
    public override void OnCollected()
    {
        scoreUpEventBus?.AddScoreRequest(score); //スコア加算イベントを発火
        base.OnCollected();
    }

    
}

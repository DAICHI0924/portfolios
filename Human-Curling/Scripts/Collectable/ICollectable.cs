using UnityEngine;
using R3;

public interface ICollectable
{
    public ItemType itemType { get; }   //自分のアイテムタイプ
    // Observable<ItemType> OnCollectedAsObservable { get; }   //取得されたときの通知を受け取るためのObservable
    void OnCollected(); //アイテムが取得されたときに呼ばれる処理
}

using UnityEngine;

namespace Kakky
{
    public class PauseEventListener : EventListenerBase
    {
        public override void Start()
        {
            //Kakky.GameEventBus.OnPause += ;
        }

        public override void OnDestroy()
        {
            //Kakky.GameEventBus.OnPause -= ;
        }
    }
}
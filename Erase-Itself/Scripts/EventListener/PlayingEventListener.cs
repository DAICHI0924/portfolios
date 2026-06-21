using Project.Key.Player;
using UnityEngine;

namespace Kakky
{
    public class PlayingEventListener : EventListenerBase
    {
        public override void Start()
        {
            GameEventBus.OnPlaying += HandlePlaying;
        }

        public void HandlePlaying()
        {
            ServiceLocator.Resolve<IPlayerService>().EnableMovement();
        }

        public override void OnDestroy()
        {
            GameEventBus.OnPlaying -= HandlePlaying;
        }
    }
}
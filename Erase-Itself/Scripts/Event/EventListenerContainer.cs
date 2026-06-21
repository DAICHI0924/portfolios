using System.Collections.Generic;
using UnityEngine;

namespace Kakky
{
    public class EventListenerContainer : MonoBehaviour
    {
        private List<IEventListener> _eventListeners = new();

        private void Awake()
        {
            CreateEventListenerInstances();
            foreach (var listener in _eventListeners)
            {
                listener.Start();
            }
        }

        private void OnDestroy()
        {
            foreach (var listener in _eventListeners)
            {
                listener.OnDestroy();
            }
        }

        private void CreateEventListenerInstances()
        {
            _eventListeners.Add(new GameStartEventListener());
            _eventListeners.Add(new GameOverEventListener());
            _eventListeners.Add(new GameClearEventListener());
            _eventListeners.Add(new PauseEventListener());
            _eventListeners.Add(new PlayingEventListener());
        }
    }
}

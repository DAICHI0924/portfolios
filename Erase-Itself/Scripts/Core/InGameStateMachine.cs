using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kakky
{
    public class InGameStateMachine : MonoBehaviour
    {
        [SerializeField, Header("")] private GameDataContext _gameDataContext;

        private IState _currentState;
        private Dictionary<InGameState, IState> _stateInstances = new Dictionary<InGameState, IState>();

        private static InGameStateMachine _instance;
        public static InGameStateMachine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<InGameStateMachine>();

                    if (_instance == null)
                    {
                        SetupStateInstance();
                    }
                }
                return _instance;
            }
        }
        private static void SetupStateInstance()
        {
            GameObject singletonObject = new GameObject(typeof(InGameStateMachine).Name);
            _instance = singletonObject.AddComponent<InGameStateMachine>();
            DontDestroyOnLoad(singletonObject);
        }

        private void Awake()
        {
            if (!RemoveDuplicates())
            {
                OnAwake();
            }
        }

        protected virtual void OnAwake()
        {
            CreateStateInstances();
        }

        private void Start()
        {
            ChangeState(InGameState.GameStart);
        }

        private void CreateStateInstances()
        {
            _stateInstances[InGameState.GameStart] = new GameStartState();
            _stateInstances[InGameState.Pause] = new PauseState();
            _stateInstances[InGameState.GameOver] = new GameOverState();
            _stateInstances[InGameState.GameClear] = new GameClearState();
            _stateInstances[InGameState.Playing] = new PlayingState();
        }

        private bool RemoveDuplicates()
        {
            if (_instance == null)
            {
                _instance = this as InGameStateMachine;
                return false;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
                return true;
            }
            return false;
        }

        public void ChangeState(InGameState newState)
        {
            if (_currentState != null)
            {
                _currentState.OnExit(_gameDataContext);
            }
            _currentState = _stateInstances[newState];
            _currentState.OnEnter(_gameDataContext);
            Debug.Log($"State changed to: {newState}");
        }

        private void OnDestroy()
        {
            if (_currentState != null)
            {
                _currentState.OnExit(_gameDataContext);
            }
            foreach (var state in _stateInstances.Values)
            {
                if (state is IDisposable disposableState)
                {
                    disposableState.Dispose();
                }
            }
        }
    }
}

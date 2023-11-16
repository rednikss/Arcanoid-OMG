using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.StateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        private readonly Dictionary<string, GameState> _states = new();
        
        private GameState _currentState;
        
        public void AddState(GameState state)
        {
            _states[state.GetType().Name] = state;
        }

        public void ChangeState<T>()
        {
            var stateType = typeof(T).Name;
            if (!_states.TryGetValue(stateType, out var state)) return;
            
            SetState(state);
        }

        private void SetState(GameState value)
        {
            _currentState?.OnExitState();
            _currentState = value;
            _currentState?.OnEnterState();
        }

        public GameState GetState<T>() => _states[typeof(T).Name];
        
        public void ExitState()
        {
            _currentState?.OnExitState();
        }
        
        public void Update()
        {
            _currentState?.Update();
        }
    }
}
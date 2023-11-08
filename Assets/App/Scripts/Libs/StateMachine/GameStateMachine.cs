using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Libs.StateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        private readonly Dictionary<string, GameState> _states = new();
        
        private GameState _currentState;
        
        public void AddState(GameState state)
        {
            state.StateMachine = this;
            _states[state.GetType().Name] = state;
        }

        public void ChangeState<T>()
        {
            var stateType = typeof(T).Name;
            if (!_states.TryGetValue(stateType, out var state)) return;
            
            SetState(state);
        }

        public void Update()
        {
            _currentState?.Update();
        }

        private void SetState(GameState value)
        {
            _currentState?.OnExitState();
            _currentState = value;
            _currentState?.OnEnterState();
        }

        public void ExitState()
        {
            _currentState?.OnExitState();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.StateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        private readonly Dictionary<string, GameState> _states = new();

        private GameState _previousState;
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

        public void ChangeToPrevious()
        {
            SetState(_previousState);
        }
        
        private async void SetState(GameState value)
        {
            _previousState = _currentState;
            _currentState = value;
            
            await (_previousState?.OnExitState() ?? Task.CompletedTask);
            await (_currentState?.OnEnterState() ?? Task.CompletedTask);
        }

        public GameState GetState<T>()
        { 
            return _states[typeof(T).Name];
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}
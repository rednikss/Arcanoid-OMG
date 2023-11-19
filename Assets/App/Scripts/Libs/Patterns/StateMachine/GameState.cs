using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.StateMachine
{
    public abstract class GameState
    {
        protected readonly GameStateMachine StateMachine;
        
        protected Dictionary<Type, MonoSystem.MonoSystem> MonoSystems = new();

        protected GameState(GameStateMachine machine)
        {
            StateMachine = machine;
        }
        
        public void AddSystem(MonoSystem.MonoSystem system) => MonoSystems.Add(system.GetType(), system);
        
        public abstract Task OnEnterState();

        public virtual void Update()
        {
            foreach (var system in MonoSystems)
            {
                if (system.Value == null)
                {
                    MonoSystems.Remove(system.Key);
                    continue;
                }
                
                system.Value.UpdateWithDT(Time.deltaTime);
            }
        }
        
        public abstract Task OnExitState();
        
    }
}
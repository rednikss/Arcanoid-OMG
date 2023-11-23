using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.StateMachine
{
    public abstract class GameState
    {
        protected readonly GameStateMachine StateMachine;

        protected readonly ServiceContainer Container;
        
        protected readonly Dictionary<Type, MonoSystem.MonoSystem> MonoSystems = new();

        protected GameState(GameStateMachine machine, ServiceContainer container)
        {
            StateMachine = machine;
            Container = container;
        }
        
        protected void AddSystem<TSystem>() where TSystem : MonoSystem.MonoSystem
        {
            MonoSystems.Add(typeof(TSystem), Container.GetService<TSystem>());
        }

        protected TSystem GetSystem<TSystem>() where TSystem : MonoSystem.MonoSystem
        {
            return (TSystem) MonoSystems[typeof(TSystem)];
        }
        
        public virtual Task OnEnterState() => Task.CompletedTask;
        
        public virtual void Update()
        {
            foreach (var system in MonoSystems)
            {
                system.Value.UpdateWithDT(Time.deltaTime);
            }
        }

        public virtual Task OnExitState() => Task.CompletedTask;
    }
}
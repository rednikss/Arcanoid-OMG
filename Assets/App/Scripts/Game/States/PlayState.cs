using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Game.Mechanics.Platform;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.ProjectContext;

namespace App.Scripts.Game.States
{
    public class PlayState : GameState
    {
        private readonly PanelManager _panelManager;

        public PlayState(GameStateMachine machine, 
            ProjectContext context, 
            Dictionary<Type, MonoSystem> systems) : base(machine)
        {
            _panelManager = context.GetContainer().GetService<PanelManager>();
            MonoSystems = systems;
        }
        
        public override Task OnEnterState()
        {
            var platform = (Platform) MonoSystems[typeof(Platform)];
            
            platform.usingBalls.Clear();
            return Task.CompletedTask;
        }

        public override Task OnExitState()
        {
            return Task.CompletedTask;
        }
    }
}
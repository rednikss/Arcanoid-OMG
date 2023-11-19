using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Scripts.Architecture.Project.InputObserver;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Game.Mechanics.Ball.Pool;
using App.Scripts.Game.Mechanics.Platform;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.ProjectContext;

namespace App.Scripts.Game.States
{
    public class StartState : GameState
    {
        private readonly ProjectContext _context;

        private readonly InputObserver inputObserver;
        private readonly PanelManager panelManager;
        
        public StartState(GameStateMachine machine, 
            ProjectContext context, 
            Dictionary<Type, MonoSystem> systems) : base(machine)
        {
            _context = context;
            MonoSystems = systems;
            
            
            inputObserver = _context.GetContainer().GetService<InputObserver>();
            panelManager = _context.GetContainer().GetService<PanelManager>();
        }
        
        public override Task OnEnterState()
        {
            var platform = (Platform) MonoSystems[typeof(Platform)];
            var pool = _context.GetContainer().GetService<BallPool>();

            if (pool == null) return Task.CompletedTask;
            
            var balls = platform.usingBalls;
            if (balls.Count == 0) balls.Add(pool.Get());

            return Task.CompletedTask;
        }

        public override void Update()
        {
            base.Update();

            if (inputObserver.IsUpInGame)
            {
                StateMachine.ChangeState<PlayState>();
            }
        }
        
        public override Task OnExitState()
        {
            
            return Task.CompletedTask;
        }
    }
}
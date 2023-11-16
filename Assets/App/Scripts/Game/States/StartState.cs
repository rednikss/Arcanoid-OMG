using System;
using System.Collections.Generic;
using App.Scripts.Architecture.Project.InputObserver;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Game.Mechanics.Ball;
using App.Scripts.Game.Mechanics.Platform;
using App.Scripts.Libs.Patterns.ObjectPool;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelInstallers.Level;

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
        
        public override void OnEnterState()
        {
            var platform = (Platform) MonoSystems[typeof(Platform)];
            var pool = _context.GetContainer().GetService<IObjectPool<Ball>>();

            platform.usingBalls.Add(pool.Get());


            if (panelManager.GetEnabledPanel<LevelPanelInstaller>() != null) return;

            var panel = panelManager.GetDisabledPanel<LevelPanelInstaller>();
            panel.gameObject.SetActive(true);
        }

        public override void Update()
        {
            base.Update();

            if (inputObserver.IsUp)
            {
                StateMachine.ChangeState<PlayState>();
            }
        }
        
        public override void OnExitState()
        {
            var platform = (Platform) MonoSystems[typeof(Platform)];
            
            platform.usingBalls.Clear();
        }
    }
}
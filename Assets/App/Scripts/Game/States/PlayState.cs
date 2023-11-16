using System;
using System.Collections.Generic;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelInstallers.Level;

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
        
        public override void OnEnterState()
        {
        }

        public override void OnExitState()
        {
            var panel = _panelManager.GetEnabledPanel<LevelPanelInstaller>();
            panel.gameObject.SetActive(true);
        }
    }
}
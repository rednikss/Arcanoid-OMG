using App.Scripts.Architecture.PanelManager;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.StateMachine;
using App.Scripts.Libs.StateMachine.MonoSystem;
using App.Scripts.UI.PanelInstallers.Level;
using UnityEngine;

namespace App.Scripts.Game.States
{
    public class PlayState : GameState
    {
        private readonly PanelManager _panelManager;
        
        private readonly MonoSystem[] defaultLevelSystems;
        
        public PlayState(ProjectContext context, MonoSystem[] defaultSystems)
        {
            _panelManager = context.GetContainer().GetService<PanelManager>();
            defaultLevelSystems = defaultSystems;
        }
        
        public override void OnEnterState()
        {
            var panel = _panelManager.GetDisabledPanel<LevelPanelInstaller>();
            panel.gameObject.SetActive(true);
        }

        public override void Update()
        {
            foreach (var monoSystem in defaultLevelSystems)
            {
                monoSystem.UpdateWithDT(Time.deltaTime);
            }
        }

        public override void OnExitState()
        {
            var panel = _panelManager.GetEnabledPanel<LevelPanelInstaller>();
            panel.gameObject.SetActive(true);
        }
    }
}
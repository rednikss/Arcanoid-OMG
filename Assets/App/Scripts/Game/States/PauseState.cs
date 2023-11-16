using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelInstallers.Pause;

namespace App.Scripts.Game.States
{
    public class PauseState : GameState
    {
        private readonly PanelManager _panelManager;
        
        public PauseState(GameStateMachine machine, 
            ProjectContext context) : base(machine)
        {
            _panelManager = context.GetContainer().GetService<PanelManager>();
        }
        
        public override void OnEnterState()
        {
            var panel = _panelManager.GetDisabledPanel<PausePanelInstaller>();
            panel.gameObject.SetActive(true);
        }

        public override void OnExitState()
        {
            var panel = _panelManager.GetEnabledPanel<PausePanelInstaller>();
            panel.gameObject.SetActive(false);
        }
    }
}
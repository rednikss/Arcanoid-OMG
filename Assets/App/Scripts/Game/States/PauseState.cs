using App.Scripts.Architecture.PanelManager;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.StateMachine;
using App.Scripts.UI.PanelInstallers.Pause;

namespace App.Scripts.Game.States
{
    public class PauseState : GameState
    {
        private readonly PanelManager _panelManager;
        
        public PauseState(ProjectContext context)
        {
            _panelManager = context.GetContainer().GetService<PanelManager>();
        }
        
        public override void OnEnterState()
        {
            var panel = _panelManager.GetDisabledPanel<PausePanelInstaller>();
            panel.gameObject.SetActive(true);
        }

        public override void Update()
        {
            
        }
        
        public override void OnExitState()
        {
            var panel = _panelManager.GetEnabledPanel<PausePanelInstaller>();
            panel.gameObject.SetActive(false);
        }
    }
}
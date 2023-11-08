using App.Scripts.Architecture.PanelManager;
using App.Scripts.Architecture.ProjectContext;
using App.Scripts.Libs.StateMachine;

namespace App.Scripts.Game.States
{
    public class PauseState : GameState
    {
        private readonly PanelManager _panelManager;
        
        private readonly string _pausePanelName;
        
        public PauseState(ProjectContext context, string pausePanelName)
        {
            _panelManager = context.GetContainer().GetService<PanelManager>();
            _pausePanelName = pausePanelName;
        }
        

        public override void OnEnterState()
        {
            _panelManager.InstallPanel(_pausePanelName);
        }

        public override void Update()
        {
            
        }
        
        public override void OnExitState()
        {
            _panelManager.DisablePanel(_pausePanelName);
        }
    }
}
using System.Threading.Tasks;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelControllers.Lose;

namespace App.Scripts.Game.States
{
    public class LoseState : GameState
    {
        private readonly PanelManager _panelManager;
        
        public LoseState(GameStateMachine machine, 
            ProjectContext context) : base(machine)
        {
            _panelManager = context.GetContainer().GetService<PanelManager>();
        }
        
        public override Task OnEnterState()
        {
            var panel = _panelManager.GetPanel<LosePanelController>();
            _panelManager.AddActive(panel);
            
            var task = panel.ShowPanel();
            return task;
        }

        public override async Task OnExitState()
        {
            var panel = _panelManager.RemoveActive();
            var task = panel.HidePanel();
            await task;
        }
    }
}
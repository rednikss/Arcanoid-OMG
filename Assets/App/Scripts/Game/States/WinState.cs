using System.Threading.Tasks;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Lose;
using App.Scripts.UI.PanelControllers.Win;

namespace App.Scripts.Game.States
{
    public class WinState : GameState
    {
        private readonly PanelManager _panelManager;
        
        public WinState(GameStateMachine machine, ServiceContainer container) : base(machine, container)
        {
            _panelManager = container.GetService<PanelManager>();
        }
        
        public override Task OnEnterState()
        {
            var panel = _panelManager.GetPanel<WinPanelController>();
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
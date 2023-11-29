using System.Threading.Tasks;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Game.Mechanics.Energy;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Game.Win;

namespace App.Scripts.Game.States
{
    public class WinState : GameState
    {
        private readonly PanelManager _panelManager;

        private readonly int energyReward;
        
        public WinState(GameStateMachine machine, ServiceContainer container, int reward) : base(machine, container)
        {
            _panelManager = container.GetService<PanelManager>();
            energyReward = reward;
        }
        
        public override Task OnEnterState()
        {
            Container.GetService<EnergyController>().AddEnergy(energyReward);
            
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
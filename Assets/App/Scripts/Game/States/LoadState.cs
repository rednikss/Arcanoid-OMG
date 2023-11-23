using System.Threading.Tasks;
using App.Scripts.Game.LevelManager;
using App.Scripts.Game.Mechanics.Blocks.Base.Pool;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Level.HealthBarController;

namespace App.Scripts.Game.States
{
    public class LoadState : GameState
    {
        public LoadState(GameStateMachine machine, ServiceContainer container) : base(machine, container)
        {
        }
        
        public override Task OnEnterState()
        {
            Container.GetService<BlockPool>().ReturnAll();
            Container.GetService<HealthBarController>().Init(Container);
            Container.GetService<LevelLoader>().LoadLevel();
            
            
            StateMachine.ChangeState<StartState>();
            return Task.CompletedTask;
        }
    }
}
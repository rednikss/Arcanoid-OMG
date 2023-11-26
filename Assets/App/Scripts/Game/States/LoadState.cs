using System.Threading.Tasks;
using App.Scripts.Game.GameObjects.Ball.Pool;
using App.Scripts.Game.GameObjects.Blocks.Base.Pool;
using App.Scripts.Game.LevelManager;
using App.Scripts.Game.LevelManager.DifficultyIncreaser;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Game.Level.HealthBarController;
using App.Scripts.UI.PanelControllers.Game.Level.PercentageController;

namespace App.Scripts.Game.States
{
    public class LoadState : GameState
    {
        public LoadState(GameStateMachine machine, ServiceContainer container) : base(machine, container)
        {
        }
        
        public override Task OnEnterState()
        {
            Container.GetService<BallPool>().ReturnAll();
            Container.GetService<BlockPool>().ReturnAll();
            Container.GetService<LevelLoader>().LoadLevel();
            Container.GetService<PercentageController>().Reset();
            Container.GetService<HealthBarController>().Init(Container);
            Container.GetService<DifficultyIncreaser>().Init(Container);
            
            StateMachine.ChangeState<StartState>();
            return Task.CompletedTask;
        }
        
        public override Task OnExitState()
        {
            return Task.CompletedTask;
        }
    }
}
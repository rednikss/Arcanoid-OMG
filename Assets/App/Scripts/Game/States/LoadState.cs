using System.Threading.Tasks;
using App.Scripts.Game.GameObjects.Ball.Pool;
using App.Scripts.Game.GameObjects.Blocks.Base.Pool;
using App.Scripts.Game.GameObjects.Boost.Base.Pool;
using App.Scripts.Game.GameObjects.Boost.Services.BallSpeedIncreaser;
using App.Scripts.Game.GameObjects.Boost.Services.BlockDestroySetter;
using App.Scripts.Game.GameObjects.Boost.Services.PlatformSizeIncreaser;
using App.Scripts.Game.GameObjects.Boost.Services.PlatformSpeedIncreaser;
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
            Container.GetService<BallSpeedIncreaser>().EndEvent();
            Container.GetService<BlockDestroySetter>().EndEvent();
            Container.GetService<PlatformSpeedIncreaser>().EndEvent();
            Container.GetService<PlatformSizeIncreaser>().EndEvent();
            
            Container.GetService<PercentageController>().Reset();
            Container.GetService<HealthBarController>().Init(Container);
            Container.GetService<DifficultyIncreaser>().Init(Container);
            
            Container.GetService<BallPool>().ReturnAll();
            Container.GetService<BlockPool>().ReturnAll();
            Container.GetService<BoostPool>().ReturnAll();

            Container.GetService<LevelLoader>().LoadLevel();
            StateMachine.ChangeState<StartState>();
            
            return Task.CompletedTask;
        }
        
        public override Task OnExitState()
        {
            return Task.CompletedTask;
        }
    }
}
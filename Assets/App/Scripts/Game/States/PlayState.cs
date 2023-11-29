using System.Threading.Tasks;
using App.Scripts.Game.GameObjects.Ball.Pool;
using App.Scripts.Game.GameObjects.Blocks.Base.Pool;
using App.Scripts.Game.GameObjects.Boost.Base.Pool;
using App.Scripts.Game.GameObjects.Boost.Services.BallSpeedIncreaser;
using App.Scripts.Game.GameObjects.Boost.Services.PlatformSizeIncreaser;
using App.Scripts.Game.GameObjects.Boost.Services.PlatformSpeedIncreaser;
using App.Scripts.Game.GameObjects.Platform;
using App.Scripts.Game.LevelManager.DifficultyIncreaser;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Game.Level.PercentageController;

namespace App.Scripts.Game.States
{
    public class PlayState : GameState
    {
        private readonly BlockPool blockPool;
        
        private readonly DifficultyIncreaser increaser;
        
        private PercentageController percentageController;

        public PlayState(GameStateMachine machine, ServiceContainer container) : base(machine, container)
        {
            AddSystem<Platform>();
            AddSystem<BallPool>();
            AddSystem<BoostPool>();

            AddSystem<BallSpeedIncreaser>();
            AddSystem<PlatformSpeedIncreaser>();
            AddSystem<PlatformSizeIncreaser>();
            
            blockPool = Container.GetService<BlockPool>();
            increaser = Container.GetService<DifficultyIncreaser>();
        }
        
        public override Task OnEnterState()
        {
            foreach (var system in MonoSystems) system.Value.IsPaused = false;

            var platform = GetSystem<Platform>();
            while (platform.RemoveBall()) { }

            percentageController = Container.GetService<PercentageController>();
            
            blockPool.OnBlockCountChanged += WinCheck;
            blockPool.OnBlockCountChanged += increaser.UpdateSpeed;
            blockPool.OnBlockCountChanged += percentageController.UpdatePercentage;

            return Task.CompletedTask;
        }

        public override Task OnExitState()
        {
            foreach (var system in MonoSystems) system.Value.IsPaused = true;
            
            blockPool.OnBlockCountChanged -= WinCheck;
            blockPool.OnBlockCountChanged -= increaser.UpdateSpeed;
            blockPool.OnBlockCountChanged -= percentageController.UpdatePercentage;
            
            return Task.CompletedTask;
        }

        private void WinCheck(int current, int min, int max)
        {
            if (current > min) return;

            StateMachine.ChangeState<WinState>();
        }
        
    }
}
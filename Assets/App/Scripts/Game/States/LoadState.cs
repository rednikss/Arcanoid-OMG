using System.Threading.Tasks;
using App.Scripts.Game.GameObjects.Ball.Pool;
using App.Scripts.Game.GameObjects.Blocks.Base.Pool;
using App.Scripts.Game.LevelManager;
using App.Scripts.Game.LevelManager.DifficultyIncreaser;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Level.HealthBarController;
using App.Scripts.UI.PanelControllers.Level.PercentageController;
using UnityEngine;

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
            Container.GetService<HealthBarController>().Init(Container);
            Container.GetService<LevelLoader>().LoadLevel();
            Container.GetService<DifficultyIncreaser>().Reset();
            Container.GetService<PercentageController>().Reset();
            
            StateMachine.ChangeState<StartState>();
            return Task.CompletedTask;
        }
        
        public override Task OnExitState()
        {
            return Task.CompletedTask;
        }
    }
}
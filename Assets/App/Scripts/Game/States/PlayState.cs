using System.Threading.Tasks;
using App.Scripts.Game.Mechanics.Ball.Pool;
using App.Scripts.Game.Mechanics.Blocks.Base.Pool;
using App.Scripts.Game.Mechanics.Platform;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.States
{
    public class PlayState : GameState
    {
        public PlayState(GameStateMachine machine, ServiceContainer container) : base(machine, container)
        {
            AddSystem<Platform>();
            AddSystem<BallPool>();
        }
        
        public override Task OnEnterState()
        {
            foreach (var system in MonoSystems) system.Value.IsPaused = false;

            var platform = GetSystem<Platform>();
            
            while (platform.RemoveBall())
            { }

            var blockPool = Container.GetService<BlockPool>();
            blockPool.OnBlockCountChanged += WinCheck;
            
            return Task.CompletedTask;
        }

        public override Task OnExitState()
        {
            foreach (var system in MonoSystems) system.Value.IsPaused = true;
            
            var blockPool = Container.GetService<BlockPool>();
            blockPool.OnBlockCountChanged -= WinCheck;
            
            return Task.CompletedTask;
        }

        private void WinCheck(int current, int min, int max)
        {
            if (current > min) return;

            StateMachine.ChangeState<WinState>();
        }
        
    }
}
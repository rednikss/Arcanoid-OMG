using System.Threading.Tasks;
using App.Scripts.Game.Mechanics.Ball.Pool;
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
            
            foreach(var ball in platform.usingBalls) ball.Velocity = Vector2.up;
            platform.usingBalls.Clear();

            return Task.CompletedTask;
        }

        public override Task OnExitState()
        {
            foreach (var system in MonoSystems) system.Value.IsPaused = true;
            return Task.CompletedTask;
        }
    }
}
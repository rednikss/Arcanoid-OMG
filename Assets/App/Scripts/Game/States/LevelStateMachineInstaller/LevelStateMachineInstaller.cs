using System;
using System.Collections.Generic;
using App.Scripts.Game.Mechanics.Ball.Receiver;
using App.Scripts.Game.Mechanics.Platform;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Level.HealthBarController;

namespace App.Scripts.Game.States.LevelStateMachineInstaller
{
    public class LevelStateMachineInstaller : MonoInstaller
    {
        private GameStateMachine gameStateMachine;

        public override void Init(ServiceContainer container)
        {
            gameStateMachine = container.GetService<GameStateMachine>();

            GameState[] states = {
                new LoadState(gameStateMachine, container),
                new StartState(gameStateMachine, container),
                new PlayState(gameStateMachine, container),
                new PauseState(gameStateMachine, container),
                new LoseState(gameStateMachine, container)
            };

            foreach (var state in states) gameStateMachine.AddState(state);

            var ballPool = container.GetService<BallReceiver>();
            ballPool.OnBallMiss += gameStateMachine.ChangeState<StartState>;
        }
    }
}
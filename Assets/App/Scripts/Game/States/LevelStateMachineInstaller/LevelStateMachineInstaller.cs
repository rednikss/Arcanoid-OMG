using App.Scripts.Game.GameObjects.Ball.Receiver;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.States.LevelStateMachineInstaller
{
    public class LevelStateMachineInstaller : MonoInstaller
    {
        [SerializeField] private int energyRewardAmount;
        
        private GameStateMachine gameStateMachine;

        public override void Init(ServiceContainer container)
        {
            gameStateMachine = container.GetService<GameStateMachine>();

            GameState[] states = {
                new LoadState(gameStateMachine, container),
                new StartState(gameStateMachine, container),
                new PlayState(gameStateMachine, container),
                new PauseState(gameStateMachine, container),
                new LoseState(gameStateMachine, container),
                new WinState(gameStateMachine, container, energyRewardAmount)
            };

            foreach (var state in states) gameStateMachine.AddState(state);

            var ballReceiver = container.GetService<BallReceiver>();
            ballReceiver.OnBallMiss += gameStateMachine.ChangeState<StartState>;
        }
    }
}
using System;
using System.Collections.Generic;
using App.Scripts.Game.Mechanics.Ball.Pool;
using App.Scripts.Game.Mechanics.Platform;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.ProjectContext;
using UnityEngine;

namespace App.Scripts.Game.States.LevelStateMachineInstaller
{
    public class LevelStateMachineInstaller : MonoInstaller
    {
        private GameStateMachine gameStateMachine;

        [SerializeReference] private MonoSystem[] systems;

        private readonly Dictionary<Type, MonoSystem> convertedSystems = new();
        
        public override void Init(ProjectContext context)
        {
            foreach (var levelSystem in systems) levelSystem.Init(context);

            foreach (var system in systems) convertedSystems.Add(system.GetType(), system);
            

            Dictionary<Type, MonoSystem> startSystems = new()
            {
                {typeof(Platform), convertedSystems[typeof(Platform)]}
            };
            
            gameStateMachine = context.GetContainer().GetService<GameStateMachine>();

            var startState = new StartState(gameStateMachine, context, startSystems);
            var playState = new PlayState(gameStateMachine, context, convertedSystems);
            var pauseState = new PauseState(gameStateMachine, context);
            
            gameStateMachine.AddState(startState);
            gameStateMachine.AddState(playState);
            gameStateMachine.AddState(pauseState);


            var ballPool = context.GetContainer().GetService<BallPool>();
            ballPool.OnReturnAllBalls += gameStateMachine.ChangeState<StartState>;
        }
    }
}
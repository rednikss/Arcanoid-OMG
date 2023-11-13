using App.Scripts.Game.States;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.StateMachine;
using App.Scripts.Libs.StateMachine.MonoSystem;
using UnityEngine;

namespace App.Scripts.Architecture.LevelStateMachineInstaller
{
    public class LevelStateMachineInstaller : MonoInstaller
    {
        private GameStateMachine gameStateMachine;

        [SerializeField] private MonoSystem[] levelSystems;
        
        public override void Init(ProjectContext context)
        {
            foreach (var levelSystem in levelSystems) levelSystem.Init(context);
            
            var playState = new PlayState(context, levelSystems);
            var pauseState = new PauseState(context);

            gameStateMachine = context.GetContainer().GetService<GameStateMachine>();
            gameStateMachine.AddState(playState);
            gameStateMachine.AddState(pauseState);
        }
    }
}
using App.Scripts.Game.States;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.StateMachine;
using App.Scripts.Libs.StateMachine.MonoSystem;
using UnityEngine;

namespace App.Scripts.Architecture.LevelStateMachineInstaller
{
    public class LevelStateMachineInstaller : MonoInstaller
    {
        [SerializeField] private GameStateMachine gameStateMachine;

        [SerializeField] private MonoSystem[] levelSystems;
        
        public override void Init(ProjectContext.ProjectContext context)
        {
            foreach (var levelSystem in levelSystems) levelSystem.Init(context);

            var playState = new PlayState(levelSystems);
            var pauseState = new PauseState(context, "Pause");
            
            gameStateMachine.AddState(playState);
            gameStateMachine.AddState(pauseState);
            gameStateMachine.ChangeState<PlayState>();
            
            context.GetContainer().SetServiceSelf(gameStateMachine);
        }
    }
}
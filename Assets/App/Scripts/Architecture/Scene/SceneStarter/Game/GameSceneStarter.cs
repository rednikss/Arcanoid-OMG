using App.Scripts.Game.States;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.ProjectContext;

namespace App.Scripts.Architecture.Scene.SceneStarter.Game
{
    public class GameSceneStarter : MonoInstaller
    {
        public override void Init(ProjectContext context)
        {
            context.GetContainer().GetService<GameStateMachine>().ChangeState<StartState>();
        }
    }
}
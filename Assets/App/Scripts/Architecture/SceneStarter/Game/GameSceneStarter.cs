using App.Scripts.Game.States;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.StateMachine;

namespace App.Scripts.Architecture.SceneStarter.Game
{
    public class GameSceneStarter : MonoInstaller
    {
        public override void Init(ProjectContext context)
        {
            context.GetContainer().GetService<GameStateMachine>().ChangeState<PlayState>();
        }
    }
}
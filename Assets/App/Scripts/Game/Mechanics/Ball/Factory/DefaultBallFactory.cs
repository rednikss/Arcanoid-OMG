using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Factory;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.ProjectContext;

namespace App.Scripts.Game.Mechanics.Ball.Factory
{
    public class DefaultBallFactory : Factory<Ball>
    {
        private GameStateMachine stateMachine;
        
        public override void Init(ProjectContext context)
        {
            stateMachine = context.GetContainer().GetService<GameStateMachine>();
        }
        
        public override Ball Create()
        {
            var newBall = Instantiate(prefab, transform);
            newBall.Init(ProjectContext.Instance);
            return newBall;
        }
    }
}
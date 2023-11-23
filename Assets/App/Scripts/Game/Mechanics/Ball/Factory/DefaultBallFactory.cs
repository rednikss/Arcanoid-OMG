using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Factory;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.ProjectContext;

namespace App.Scripts.Game.Mechanics.Ball.Factory
{
    public class DefaultBallFactory : Factory<Ball>
    {
        private ServiceContainer _container;
        
        public override void Init(ServiceContainer container)
        {
            _container = container;
        }
        
        public override Ball Create()
        {
            var newBall = Instantiate(prefab, transform);
            newBall.Init(_container);
            
            return newBall;
        }
    }
}
using App.Scripts.Libs.Patterns.Factory;
using App.Scripts.Libs.Patterns.Service.Container;

namespace App.Scripts.Game.GameObjects.Ball.Factory
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
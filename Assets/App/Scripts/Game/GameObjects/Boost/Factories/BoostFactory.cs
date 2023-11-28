using App.Scripts.Libs.Patterns.Factory;
using App.Scripts.Libs.Patterns.Service.Container;

namespace App.Scripts.Game.GameObjects.Boost.Factories
{
    public class BoostFactory : Factory<Base.Boost>
    {
        private ServiceContainer _container;

        public override void Init(ServiceContainer container)
        {
            _container = container;
        }

        public override Base.Boost Create()
        {
            var newBlock = Instantiate(prefab, transform);
            newBlock.Init(_container);

            return newBlock;
        }
    }
}
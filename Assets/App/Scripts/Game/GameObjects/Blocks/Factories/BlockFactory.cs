using App.Scripts.Game.GameObjects.Blocks.Base;
using App.Scripts.Libs.Patterns.Factory;
using App.Scripts.Libs.Patterns.Service.Container;

namespace App.Scripts.Game.GameObjects.Blocks.Factories
{
    public class BlockFactory : Factory<Block>
    {
        private ServiceContainer _container;

        public override void Init(ServiceContainer container)
        {
            _container = container;
        }

        public override Block Create()
        {
            var newBlock = Instantiate(prefab, transform);
            newBlock.Init(_container);

            return newBlock;
        }
    }
}
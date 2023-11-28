using App.Scripts.Libs.Patterns.ObjectPool;
using App.Scripts.Libs.Patterns.Service.Container;

namespace App.Scripts.Game.GameObjects.Boost.Base.Pool
{
    public class BoostPool : ObjectPool<Boost>
    {
        private ServiceContainer _container;
        
        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            _container = container;
        }
        
        public override void TakeObject(Boost pooledObject, int id = 0)
        {
            pooledObject.Init(_container);
            base.TakeObject(pooledObject, id);
        }

        public void ReturnAll()
        {
            while (UsingObjects.Count > 0)
            {
                var boost = UsingObjects[0];
                ReturnObject(boost, boost.ID);
            }
        }
    }
}
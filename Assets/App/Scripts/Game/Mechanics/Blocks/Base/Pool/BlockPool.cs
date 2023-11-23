using System;
using App.Scripts.Libs.Patterns.ObjectPool;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Blocks.Base.Pool
{
    public class BlockPool : ObjectPool<Block>
    {
        private ServiceContainer _container;
        
        public event Action<int, int, int> OnBlockCountChanged;

        private int maxBlockCount;
        private int minBlockCount;

        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            _container = container;
            //maxBlockCount = container.GetService<LevelLoader>().GetLevelBlockCount();
        }

        public override void ReturnObject(Block pooledObject, int id = 0)
        {
            pooledObject.transform.localScale = Vector3.one;
            base.ReturnObject(pooledObject, id);
            
            if (pooledObject.scriptable.blockID == 0) minBlockCount--;
            
            OnBlockCountChanged?.Invoke(UsingObjects.Count, minBlockCount, maxBlockCount);
        }

        public override void TakeObject(Block pooledObject, int id = 0)
        {
            pooledObject.Init(_container);
            base.TakeObject(pooledObject, id);
            
            if (pooledObject.scriptable.blockID == 0) minBlockCount++;
            
            OnBlockCountChanged?.Invoke(UsingObjects.Count, minBlockCount, maxBlockCount);
        }

        public void ReturnAll()
        {
            while (UsingObjects.Count > 0)
            {
                var block = UsingObjects[0];
                ReturnObject(block, block.scriptable.blockID);
            }
        }
    }
}
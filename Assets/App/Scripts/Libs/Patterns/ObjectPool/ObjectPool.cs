using System.Collections.Generic;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.ObjectPool.Scriptable;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.ObjectPool
{
    public abstract class ObjectPool<TObjectType> : MonoInstaller, IObjectPool<TObjectType>
    {
        [SerializeField] protected ObjectPoolDataScriptable scriptable;

        protected readonly Stack<TObjectType> PoolObjects = new();
        
        protected readonly List<TObjectType> UsingObjects = new();
        
        public abstract TObjectType Create();
        
        public abstract TObjectType Get();
        
        public abstract void Release(TObjectType pooledObject);

        
        public abstract void OnReturnObject(TObjectType pooledObject);
        
        public abstract void OnTakeObject(TObjectType pooledObject);
        
        public abstract void OnDestroyObject(TObjectType pooledObject);
        
        public abstract void Clear(bool clearUsing);
        
    }
}
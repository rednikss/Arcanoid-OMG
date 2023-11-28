using System.Collections.Generic;
using App.Scripts.Libs.Patterns.Factory;
using App.Scripts.Libs.Patterns.ObjectPool.Scriptable;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.ObjectPool
{
    public abstract class ObjectPool<TObjectType> : MonoSystem, IObjectPool<TObjectType> where TObjectType : MonoBehaviour
    {
        [SerializeField] protected ObjectPoolDataScriptable scriptable;
        
        [SerializeField] protected Factory<TObjectType>[] factories;

        private readonly List<Stack<TObjectType>> poolObjects = new();
        
        protected readonly List<TObjectType> UsingObjects = new();
        public int ActiveCount => UsingObjects.Count;
        
        public override void Init(ServiceContainer container)
        {
            for (int i = 0; i < factories.Length; i++)
            {
                poolObjects.Add(new());
                
                for (int j = 0; j < scriptable.defaultSize / factories.Length; j++) Create(i);
            }
        }
        
        public virtual TObjectType Create(int id = 0)
        {
            if (poolObjects.Count + UsingObjects.Count > scriptable.maximumSize)
            {
                Debug.LogError($"{GetType()} is full!");
                return null;
            }
            
            var newBall = factories[id].Create();
            ReturnObject(newBall, id);
            
            return newBall;
        }

        public virtual TObjectType Get(int id = 0)
        {
            if (!poolObjects[id].TryPeek(out TObjectType newBall)) newBall = Create(id);
            
            TakeObject(newBall, id);

            return newBall;
        }

        public virtual void ReturnObject(TObjectType pooledObject, int id = 0)
        {
            pooledObject.gameObject.SetActive(false);
            
            UsingObjects.Remove(pooledObject);
            poolObjects[id].Push(pooledObject);
        }

        public virtual void TakeObject(TObjectType pooledObject, int id = 0)
        {
            pooledObject.gameObject.SetActive(true);

            poolObjects[id].Pop();
            UsingObjects.Add(pooledObject);
        }

        public virtual void DestroyObject(TObjectType pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }
        
        public virtual void Clear(bool clearUsing)
        {
            foreach (var typedPool in poolObjects)
            {
                while (typedPool.Count > 0)
                {
                    DestroyObject(typedPool.Pop());
                }
            }
            
            if (!clearUsing) return;
            
            while (UsingObjects.Count > 0)
            {
                var obj = UsingObjects[0];
                UsingObjects.Remove(obj);
                
                DestroyObject(obj);
            }
        }
    }
}
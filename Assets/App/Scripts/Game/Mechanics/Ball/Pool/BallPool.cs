using System;
using App.Scripts.Game.Mechanics.Ball.Factory;
using App.Scripts.Libs.Patterns.ObjectPool;
using App.Scripts.Libs.ProjectContext;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Mechanics.Ball.Pool
{
    public class BallPool : ObjectPool<Ball>
    {
        [SerializeField] private DefaultBallFactory factory;

        public event Action OnReturnAllBalls;
        
        public override void Init(ProjectContext context)
        {
            for (int i = 0; i < scriptable.defaultSize; i++) Create();
        }

        public override Ball Create()
        {
            if (PoolObjects.Count + UsingObjects.Count > scriptable.maximumSize)
            {
                Debug.LogError($"Pool is full!");
                return null;
            }
            
            var newBall = factory.Create();
            ReturnObject(newBall);
            
            return newBall;
        }

        public override Ball Get()
        {
            var newBall = PoolObjects.Peek() ?? Create();
            TakeObject(newBall);

            return newBall;
        }

        public override void ReturnObject(Ball pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
            
            UsingObjects.Remove(pooledObject);
            
            if (UsingObjects.Count == 0) OnReturnAllBalls?.Invoke();
            
            PoolObjects.Push(pooledObject);
        }

        public override void TakeObject(Ball pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
            
            float angle = Mathf.Lerp(45, 135, Random.value);
            pooledObject.SetVelocity(Quaternion.Euler(0, 0, angle) * Vector3.right);

            PoolObjects.Pop();
            UsingObjects.Add(pooledObject);
        }

        public override void DestroyObject(Ball pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        public void OnDestroy()
        {
            Clear(true);
        }

        public override void Clear(bool clearUsing)
        {
            while (PoolObjects.Count > 0)
            {
                DestroyObject(PoolObjects.Pop());
            }
            
            if (!clearUsing) return;
            
            while (UsingObjects.Count > 0)
            {
                var obj = UsingObjects[0];
                UsingObjects.Remove(obj);
                
                DestroyObject(obj);
            }
        }

        public override void UpdateWithDT(float dt)
        {
            foreach (var system in UsingObjects)
            {
                system.UpdateWithDT(dt);
            }
        }
    }
}
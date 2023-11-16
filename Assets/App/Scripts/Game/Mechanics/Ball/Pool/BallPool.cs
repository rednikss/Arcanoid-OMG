using App.Scripts.Game.Mechanics.Ball.Factory;
using App.Scripts.Libs.Patterns.ObjectPool;
using App.Scripts.Libs.ProjectContext;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Ball.Pool
{
    public class BallPool : ObjectPool<Ball>
    {
        [SerializeField] private DefaultBallFactory factory;
        
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
            PoolObjects.Push(newBall);
            
            return newBall;
        }

        public override Ball Get()
        {
            var newBall = PoolObjects.Peek() ?? Create();
            OnTakeObject(newBall);

            return newBall;
        }   

        public override void Release(Ball pooledObject)
        {
            OnReturnObject(pooledObject);
        }

        public override void OnReturnObject(Ball pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
            
            PoolObjects.Push(pooledObject);
            UsingObjects.Remove(pooledObject);
        }

        public override void OnTakeObject(Ball pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
            float angle = Mathf.Lerp(45, 135, Random.value);
            pooledObject.SetVelocity(Quaternion.Euler(0, 0, angle) * Vector3.right);

            PoolObjects.Pop();
            UsingObjects.Add(pooledObject);
        }

        public override void OnDestroyObject(Ball pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        public override void Clear(bool clearUsing)
        {
            while (PoolObjects.Count > 0)
            {
                OnDestroyObject(PoolObjects.Pop());
            }
            
            if (!clearUsing) return;
            
            while (UsingObjects.Count > 0)
            {
                var obj = UsingObjects[0];
                UsingObjects.Remove(obj);
                
                OnDestroyObject(obj);
            }
        }
    }
}
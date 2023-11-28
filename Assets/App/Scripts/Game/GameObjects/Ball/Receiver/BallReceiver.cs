using System;
using App.Scripts.Game.GameObjects.Ball.Pool;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Camera.Adapter;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Ball.Receiver
{
    public class BallReceiver : MonoInstaller
    {
        [SerializeField] private BoxCollider2D _collider;
        
        private BallPool pool;
        
        public event Action OnBallMiss;
        
        public override void Init(ServiceContainer container)
        {
            var adapter = container.GetService<CameraAdapter>();
            
            var size = _collider.size;
            size.x = 2 * adapter.GetSize().x;
            size.y = 2;
            _collider.size = size;
            transform.position = new(0, -adapter.GetSize().y - 1, 0);
            
            pool = container.GetService<BallPool>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.TryGetComponent<Ball>(out var ball)) return;
            
            pool.ReturnObject(ball);
            
            if (pool.ActiveCount > 0) return;
            
            OnBallMiss?.Invoke();
        }
    }
}
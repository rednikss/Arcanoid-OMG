using App.Scripts.Game.GameObjects.Boost.Base.Pool;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Base
{
    public class Boost : MonoSystem
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private int id;

        [SerializeField] private Vector2 moveDirection;
        [SerializeField] [Min(1)] private float speed;
        public int ID => id;

        private BoostPool boostPool;
        protected ServiceContainer Container;
        
        public override void Init(ServiceContainer container)
        {
            boostPool = container.GetService<BoostPool>();
            Container = container;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out Platform.Platform _)) return;
            
            OnCollected();
        }

        private void OnBecameInvisible()
        {
            if (_rigidbody.velocity != Vector2.zero) boostPool.ReturnObject(this, ID);
        }

        public override void UpdateWithDT(float dt)
        {
            _rigidbody.MovePosition(_rigidbody.position + dt * moveDirection.normalized * speed);
        }
        
        protected virtual void OnCollected()
        {
            boostPool.ReturnObject(this, ID);
        }
    }
}
using System;
using App.Scripts.Game.Mechanics.Ball.Pool;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.ProjectContext;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Ball
{
    public class Ball : MonoSystem
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] [Min(0)] private float speed;
        
        private Vector2 _currentVelocity;

        private BallPool _pool;
        public override void Init(ProjectContext context)
        {
            _pool = context.GetContainer().GetService<BallPool>();
        }

        public override void UpdateWithDT(float dt)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.MovePosition(_rigidbody.position + dt * _currentVelocity);
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            Vector2 normal = Vector2.zero;
            foreach (var contact in col.contacts)
            {
                normal += contact.normal;
            }
            normal.Normalize();
            
            _currentVelocity = Vector2.Reflect(_currentVelocity, normal);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _currentVelocity = velocity.normalized * speed;
        }
        
        public void SetSpeed(float newSpeed)
        {
            speed = Math.Abs(newSpeed);
            _currentVelocity = _currentVelocity.normalized * speed;
        }

        private void OnBecameInvisible()
        {
            _pool.ReturnObject(this);
        }
    }
}
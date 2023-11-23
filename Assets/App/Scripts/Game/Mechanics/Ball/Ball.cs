using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Ball
{
    public class Ball : MonoSystem
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] [Min(0)] private float speed;
        
        private Vector2 _currentVelocity;

        public Vector2 Velocity
        {
            get => _rigidbody.velocity;
            set => _rigidbody.velocity = value.normalized * speed;
        }
        
        public override void Init(ServiceContainer container)
        {
            
        }

        public override void UpdateWithDT(float dt)
        {
            if (IsPaused) return;
            
            SetSpeed(speed);
            _currentVelocity = _rigidbody.velocity;
        }

        protected override void PauseSystem()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        protected override void ResumeSystem()
        {
            _rigidbody.velocity = _currentVelocity;
        }

        public void SetSpeed(float newSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * newSpeed;
        }
    }
}
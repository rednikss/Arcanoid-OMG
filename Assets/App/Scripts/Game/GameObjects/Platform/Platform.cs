using System.Collections.Generic;
using App.Scripts.Architecture.Project.InputObserver;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Platform
{
    public class Platform : MonoSystem
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [SerializeField] [Min(0)] private float speed;

        [SerializeField] private Transform containerTransform;
        
        private InputObserver inputObserver;

        private readonly List<Ball.Ball> usingBalls = new();
        
        public override void Init(ServiceContainer container)
        {
            inputObserver = container.GetService<InputObserver>();
        }

        public override void UpdateWithDT(float dt)
        {
            foreach (var ball in usingBalls) ball.transform.position = containerTransform.position;
            
            if (!inputObserver.IsPressedInGame) return;
            
            var targetPos = inputObserver.GetWorldPosition();

            float velocity = Mathf.Clamp((targetPos - transform.position).x, -1, 1);
            velocity *= speed;
            
            _rigidbody.MovePosition(_rigidbody.position + Vector2.right * (dt * velocity));
        }

        public void AddBall(Ball.Ball ball)
        {
            usingBalls.Add(ball);
            ball.transform.position = containerTransform.position;
        }
        
        public bool RemoveBall()
        {
            if (usingBalls.Count == 0) return false;

            var ball = usingBalls[0];
            ball.Velocity = Vector2.up;
            
            usingBalls.Remove(ball);
            return true;
        }
    }
}
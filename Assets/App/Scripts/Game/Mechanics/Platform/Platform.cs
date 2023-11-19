using System.Collections.Generic;
using App.Scripts.Architecture.Project.InputObserver;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.ProjectContext;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Platform
{
    public class Platform : MonoSystem
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [SerializeField] [Min(0)] private float speed;

        [SerializeField] private Transform containerTransform;
        
        private InputObserver inputObserver;

        [HideInInspector]
        public List<Ball.Ball> usingBalls = new();
        
        public override void Init(ProjectContext context)
        {
            inputObserver = context.GetContainer().GetService<InputObserver>();
        }

        public override void UpdateWithDT(float dt)
        {
            bool isPressed = inputObserver.IsPressedInGame;
            var targetPos = isPressed ? inputObserver.GetWorldPosition() : transform.position;

            float velocity = Mathf.Clamp((targetPos - transform.position).x, -1, 1);
            velocity *= speed;
            
            _rigidbody.velocity = Vector2.right * velocity;

            foreach (var ball in usingBalls) ball.transform.position = containerTransform.position;
        }

    }
}
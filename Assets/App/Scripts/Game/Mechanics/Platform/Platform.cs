﻿using System.Collections.Generic;
using App.Scripts.Architecture.Project.InputObserver;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.Libs.Patterns.Service.Container;
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
            
            _rigidbody.MovePosition(_rigidbody.position + dt * Vector2.right * velocity);
        }

    }
}
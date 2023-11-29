using System.Collections.Generic;
using App.Scripts.Architecture.Project.InputObserver;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base.Scriptable;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Platform
{
    public class Platform : MonoSystem
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [SerializeField] [Min(0)] private float speed;

        [SerializeField] private Transform containerTransform;

        [SerializeField] private Transform[] scaledComponents;
        
        [SerializeField] private AnimationOptionsScriptable scaleOptions;
        
        private InputObserver inputObserver;

        private readonly List<Ball.Ball> usingBalls = new();

        private float _currentSpeed;
        
        public override void Init(ServiceContainer container)
        {
            inputObserver = container.GetService<InputObserver>();
            _currentSpeed = speed;
        }

        public override void UpdateWithDT(float dt)
        {
            foreach (var ball in usingBalls) ball.transform.position = containerTransform.position;
            
            if (!inputObserver.IsPressedInGame) return;
            
            var targetPos = inputObserver.GetWorldPosition();

            float velocity = Mathf.Clamp((targetPos - transform.position).x, -1, 1);
            velocity *= _currentSpeed;
            
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
            
            usingBalls.RemoveAt(0);
            return true;
        }

        public void SetSpeedPercent(float percent)
        {
            _currentSpeed = speed * percent;
        }

        public void SetWidthPercent(float percent)
        {
            foreach (var comp in scaledComponents)
            {
                for (int i = 0; i < comp.childCount; i++)
                {
                    comp.GetChild(i).DOScaleX(1 / percent, scaleOptions.animationTime)
                        .SetEase(scaleOptions.showEase);
                }

                comp.DOScaleX(percent, scaleOptions.animationTime)
                    .SetEase(scaleOptions.showEase);
            }
        }
    }
}
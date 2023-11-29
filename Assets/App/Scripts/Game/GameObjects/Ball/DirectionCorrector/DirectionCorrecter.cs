using System;
using System.Collections.Generic;
using App.Scripts.Game.GameObjects.Ball.DirectionCorrector.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Ball.DirectionCorrector
{
    public class DirectionCorrecter : MonoInstaller
    {
        [SerializeField] private DirectionCorrectionScriptable scriptable;

        [SerializeField] private Ball ball;

        private readonly List<DirectionZone> blockZones = new();
        
        public override void Init(ServiceContainer container)
        {
            blockZones.AddRange(scriptable.directions);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (ball.IsPaused || other.gameObject.TryGetComponent<Platform.Platform>(out _)) return;
            
            ball.Velocity = CorrectVelocity(ball.Velocity, blockZones);
        }

        private Vector2 CorrectVelocity(Vector2 velocity, List<DirectionZone> zones)
        {
            foreach (var zone in zones)
            {
                float differenceAngle = Vector2.Angle(zone.Direction, velocity);
                if (Math.Abs(Math.Round(differenceAngle)) >= zone.Width) continue;

                var offsetAngle = Mathf.Sign(differenceAngle) * zone.Width;
                velocity = Quaternion.Euler(0, 0, offsetAngle) * zone.Direction;
            }
            
            return velocity;
        }

    }
}
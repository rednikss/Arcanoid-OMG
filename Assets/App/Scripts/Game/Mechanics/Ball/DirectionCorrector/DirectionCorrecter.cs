using System.Collections.Generic;
using App.Scripts.Game.Mechanics.Ball.DirectionCorrector.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Ball.DirectionCorrector
{
    public class DirectionCorrecter : MonoInstaller
    {
        [SerializeField] private DirectionCorrectionScriptable scriptable;

        [SerializeField] private Ball ball;

        private List<DirectionZone> blockZones = new();
        
        public override void Init(ServiceContainer container)
        {
            blockZones.AddRange(scriptable.directions);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Platform.Platform>(out _)) return;
            
            ball.Velocity = CorrectVelocity(ball.Velocity, blockZones);
        }

        private Vector2 CorrectVelocity(Vector2 velocity, List<DirectionZone> zones)
        {
            foreach (var zone in zones)
            {
                float differenceAngle = Vector2.Angle(velocity, zone.Direction);
                if (Mathf.Abs(differenceAngle) >= zone.Width) continue;

                var offsetAngle = Mathf.Sign(differenceAngle) * zone.Width;
                velocity = Quaternion.Euler(0, 0, offsetAngle) * velocity;
            }
            
            return velocity;
        }

    }
}
using App.Scripts.Game.Mechanics.Ball.Factory;
using App.Scripts.Libs.Patterns.Factory;
using App.Scripts.Libs.Patterns.ObjectPool;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Ball.Pool
{
    public class BallPool : ObjectPool<Ball>
    {
        public override void UpdateWithDT(float dt)
        {
            foreach (var system in UsingObjects)
            {
                system.UpdateWithDT(dt);
            }
        }

        protected override void PauseSystem()
        {
            foreach (var system in UsingObjects) system.IsPaused = true;
        }
        
        protected override void ResumeSystem()
        {
            foreach (var system in UsingObjects) system.IsPaused = false;
        }
    }
}
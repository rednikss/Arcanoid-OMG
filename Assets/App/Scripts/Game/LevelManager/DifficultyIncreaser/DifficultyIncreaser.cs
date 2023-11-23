using App.Scripts.Game.GameObjects.Ball.Pool;
using App.Scripts.Game.LevelManager.DifficultyIncreaser.Scriptables.SpeedRange;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.LevelManager.DifficultyIncreaser
{
    public class DifficultyIncreaser : MonoInstaller
    {
        [SerializeField] private SpeedRangeScriptable speedRange;

        public float Speed { get; private set; }

        private BallPool pool;
        
        public override void Init(ServiceContainer container)
        {
            Speed = speedRange.minSpeed;
            
            pool = container.GetService<BallPool>();
        }

        public void UpdateSpeed(int current, int min, int max)
        {
            var percent = 1 - (float) current / max;

            Speed = Mathf.Lerp(speedRange.minSpeed, speedRange.maxSpeed, percent);
            pool.SetSpeed(Speed);
        }

        public void Reset() => pool.SetSpeed(speedRange.minSpeed);
    }
}
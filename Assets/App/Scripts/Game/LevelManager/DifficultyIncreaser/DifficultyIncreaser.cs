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

        private SpeedRangeScriptable _currentRange;

        private int lastPercent;
        
        public float Speed { get; private set; }

        private BallPool pool;
        
        public override void Init(ServiceContainer container)
        {
            Speed = speedRange.minSpeed;
            _currentRange = speedRange;
            lastPercent = 0;
            
            pool = container.GetService<BallPool>();
        }

        public void UpdateSpeed(int current, int min, int max)
        {
            var percent = 1 - (float) current / max;
            lastPercent = (int) (percent * 100);
            
            Speed = Mathf.Lerp(_currentRange.minSpeed, _currentRange.maxSpeed, percent);
            pool.SetSpeed(Speed);
        }

        public void SetRange(SpeedRangeScriptable scriptable)
        {
            _currentRange = scriptable;
            UpdateSpeed(lastPercent, 0, 100);
        }

        public void ResetRange() => SetRange(speedRange);
        
        
        public void Reset() => pool.SetSpeed(speedRange.minSpeed);
    }
}
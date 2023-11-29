using App.Scripts.Game.LevelManager.DifficultyIncreaser;
using App.Scripts.Game.LevelManager.DifficultyIncreaser.Scriptables.SpeedRange;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Services.BallSpeedIncreaser
{
    public class BallSpeedIncreaser : TemporaryEffect.TemporaryEffect<SpeedRangeScriptable>
    {
        private DifficultyIncreaser difficultyIncreaser;
        
        public override void Init(ServiceContainer container)
        {
            difficultyIncreaser = container.GetService<DifficultyIncreaser>();
        }
        
        public override void StartEvent(SpeedRangeScriptable speedRange)
        {
            difficultyIncreaser.SetRange(speedRange);
            CurrentTime = eventDuration;
        }

        public override void EndEvent()
        {
            difficultyIncreaser.ResetRange();
            
            base.EndEvent();
        }
    }
}
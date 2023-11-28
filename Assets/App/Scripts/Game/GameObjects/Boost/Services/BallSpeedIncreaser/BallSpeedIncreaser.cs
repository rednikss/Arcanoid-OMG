using App.Scripts.Game.LevelManager.DifficultyIncreaser;
using App.Scripts.Game.LevelManager.DifficultyIncreaser.Scriptables.SpeedRange;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Services.BallSpeedIncreaser
{
    public class BallSpeedIncreaser : TemporaryEffect.TemporaryEffect
    {
        [SerializeField] private SpeedRangeScriptable scriptable;

        private DifficultyIncreaser difficultyIncreaser;
        
        public override void Init(ServiceContainer container)
        {
            difficultyIncreaser = container.GetService<DifficultyIncreaser>();
        }
        
        public override void StartEvent()
        {
            difficultyIncreaser.SetRange(scriptable);
            
            base.StartEvent();
        }

        protected override void EndEvent()
        {
            difficultyIncreaser.ResetRange();
            
            base.EndEvent();
        }
    }
}
using App.Scripts.Game.GameObjects.Boost.Services.BallSpeedIncreaser;
using App.Scripts.Game.LevelManager.DifficultyIncreaser.Scriptables.SpeedRange;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Types.BallSpeed
{
    public class BallSpeedIncreaseBoost : Base.Boost
    {
        [SerializeField] private SpeedRangeScriptable percentIncrease;

        protected override void OnCollected()
        {
            base.OnCollected();
            
            Container.GetService<BallSpeedIncreaser>().StartEvent(percentIncrease);
        }
    }
}
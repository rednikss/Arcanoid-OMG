using App.Scripts.Game.GameObjects.Boost.Services.PlatformSpeedIncreaser;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Types.PlatformSpeed
{
    public class PlatformSpeedIncreaseBoost : Base.Boost
    {
        [SerializeField] [Range(-1, 1)] private float percent;

        protected override void OnCollected()
        {
            base.OnCollected();
            
            Container.GetService<PlatformSpeedIncreaser>().StartEvent(percent);
        }
    }
}
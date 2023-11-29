using App.Scripts.Game.GameObjects.Boost.Services.PlatformSizeIncreaser;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Types.PlatformSize
{
    public class PlatformSizeIncreaseBoost : Base.Boost
    {
        [SerializeField] [Min(0)] private float percent;

        protected override void OnCollected()
        {
            base.OnCollected();
            
            Container.GetService<PlatformSizeIncreaser>().StartEvent(percent);
        }
    }
}
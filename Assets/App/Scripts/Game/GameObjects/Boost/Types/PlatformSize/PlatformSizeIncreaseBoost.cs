using App.Scripts.Game.GameObjects.Boost.Services.PlatformSizeIncreaser;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Types.PlatformSize
{
    public class PlatformSizeIncreaseBoost : Base.Boost
    {
        [SerializeField] [Range(-1, 1)] private float percent;
        
        protected override void OnCollected()
        {
            Container.GetService<PlatformSizeIncreaser>().StartEvent(percent);
            
            base.OnCollected();
        }
    }
}
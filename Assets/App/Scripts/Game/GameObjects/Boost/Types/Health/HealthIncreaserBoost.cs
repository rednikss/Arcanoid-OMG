using App.Scripts.UI.PanelControllers.Game.Level.HealthBarController;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Types.Health
{
    public class HealthIncreaserBoost : Base.Boost
    {
        [SerializeField] private int addCount;
        
        protected override void OnCollected()
        {
            Container.GetService<HealthBarController>().SafeAddHeart(addCount);
            
            base.OnCollected();
        }
    }
}
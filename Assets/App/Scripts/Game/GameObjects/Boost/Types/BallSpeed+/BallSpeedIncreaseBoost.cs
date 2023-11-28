using App.Scripts.Game.GameObjects.Boost.Services.BallSpeedIncreaser;

namespace App.Scripts.Game.GameObjects.Boost.Types.BallSpeed_
{
    public class BallSpeedIncreaseBoost : Base.Boost
    {
        
        protected override void OnCollected()
        {
            Container.GetService<BallSpeedIncreaser>().StartEvent();
            
            base.OnCollected();
        }
    }
}
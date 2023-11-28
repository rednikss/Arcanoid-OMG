using App.Scripts.Game.GameObjects.Boost.Services.BallSpeedDecreaser;

namespace App.Scripts.Game.GameObjects.Boost.Types.BallSpeed_
{
    public class BallSpeedDecreaseBoost : Base.Boost
    {
        
        protected override void OnCollected()
        {
            Container.GetService<BallSpeedDecreaser>().StartEvent();
            
            base.OnCollected();
        }
    }
}
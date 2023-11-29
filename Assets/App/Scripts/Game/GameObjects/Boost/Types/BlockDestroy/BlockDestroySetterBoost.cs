using App.Scripts.Game.GameObjects.Boost.Services.BlockDestroySetter;

namespace App.Scripts.Game.GameObjects.Boost.Types.BlockDestroy
{
    public class BlockDestroySetterBoost : Base.Boost
    {
        protected override void OnCollected()
        {
            Container.GetService<BlockDestroySetter>().StartEvent(true);
            
            base.OnCollected();
        }
    }
}
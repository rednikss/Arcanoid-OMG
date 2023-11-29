using App.Scripts.Game.GameObjects.Ball.Pool;
using App.Scripts.Game.GameObjects.Blocks.Base.Pool;
using App.Scripts.Libs.Patterns.Service.Container;

namespace App.Scripts.Game.GameObjects.Boost.Services.BlockDestroySetter
{
    public class BlockDestroySetter : TemporaryEffect.TemporaryEffect<bool>
    {
        private BlockPool blockPool;
        private BallPool ballPool;
        
        public override void Init(ServiceContainer container)
        {
            blockPool = container.GetService<BlockPool>();
            ballPool = container.GetService<BallPool>();
        }
        
        public override void StartEvent(bool damage)
        {
            blockPool.SetImmediateDestroy(true);
            ballPool.SetBoostView(true);
            
            CurrentTime = eventDuration;
        }

        public override void EndEvent()
        {
            blockPool.SetImmediateDestroy(false);
            ballPool.SetBoostView(false);
            
            base.EndEvent();
        }
    }
}
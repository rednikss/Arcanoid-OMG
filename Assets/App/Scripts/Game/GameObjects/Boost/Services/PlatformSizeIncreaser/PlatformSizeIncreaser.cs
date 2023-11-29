using App.Scripts.Libs.Patterns.Service.Container;

namespace App.Scripts.Game.GameObjects.Boost.Services.PlatformSizeIncreaser
{
    public class PlatformSizeIncreaser : TemporaryEffect.TemporaryEffect<float>
    {
        private Platform.Platform platform;
        
        public override void Init(ServiceContainer container)
        {
            platform = container.GetService<Platform.Platform>();
        }
        
        public override void StartEvent(float speedPercent)
        {
            platform.SetWidthPercent(1 + speedPercent);
            CurrentTime = eventDuration;
        }

        public override void EndEvent()
        {
            platform.SetWidthPercent(1);
            
            base.EndEvent();
        }
    }
}
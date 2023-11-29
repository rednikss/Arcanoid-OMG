using App.Scripts.Libs.Patterns.Service.Container;

namespace App.Scripts.Game.GameObjects.Boost.Services.PlatformSpeedIncreaser
{
    public class PlatformSpeedIncreaser : TemporaryEffect.TemporaryEffect<float>
    {
        private Platform.Platform platform;
        
        public override void Init(ServiceContainer container)
        {
            platform = container.GetService<Platform.Platform>();
        }
        
        public override void StartEvent(float speedPercent)
        {
            platform.SetSpeedPercent(1 + speedPercent);
            CurrentTime = eventDuration;
        }

        public override void EndEvent()
        {
            platform.SetSpeedPercent(1);
            
            base.EndEvent();
        }
    }
}
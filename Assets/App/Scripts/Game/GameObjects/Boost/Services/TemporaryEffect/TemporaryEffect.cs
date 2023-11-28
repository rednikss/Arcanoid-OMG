using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Services.TemporaryEffect
{
    public abstract class TemporaryEffect : MonoInstaller
    {
        [SerializeField] [Min(0)] protected float eventDuration;

        private float currentTime;
        
        private void Update()
        {
            if (currentTime == 0) return;
            
            currentTime -= Time.unscaledDeltaTime;

            if (currentTime > 0) return;
            
            EndEvent();
        }

        public virtual void StartEvent()
        {
            currentTime = eventDuration;
        }

        protected virtual void EndEvent()
        {
            currentTime = 0;
        }
    }
}
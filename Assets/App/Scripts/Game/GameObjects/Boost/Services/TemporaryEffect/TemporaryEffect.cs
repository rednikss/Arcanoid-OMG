using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Services.TemporaryEffect
{
    public abstract class TemporaryEffect<TDataType> : MonoSystem
    {
        [SerializeField] [Min(0)] protected float eventDuration;

        protected float CurrentTime;

        public override void UpdateWithDT(float dt)
        {
            if (CurrentTime < 0) return;

            CurrentTime -= dt;

            if (CurrentTime > 0) return;

            EndEvent();
        }

        public abstract void StartEvent(TDataType data);

        public virtual void EndEvent()
        {
            CurrentTime = -1;
        }
    }
}
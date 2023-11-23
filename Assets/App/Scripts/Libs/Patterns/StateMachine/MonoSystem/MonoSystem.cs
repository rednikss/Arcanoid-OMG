using App.Scripts.Libs.EntryPoint.MonoInstaller;

namespace App.Scripts.Libs.Patterns.StateMachine.MonoSystem
{
    public abstract class MonoSystem : MonoInstaller, IUpdatable
    {
        private bool _isPaused;
        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                
                if (value) PauseSystem();
                else ResumeSystem();
            }
        }

        public virtual void UpdateWithDT(float dt)
        {
            
        }
        

        protected virtual void PauseSystem()
        {
        
        }

        protected virtual void ResumeSystem()
        {
            
        }
        
    }
}
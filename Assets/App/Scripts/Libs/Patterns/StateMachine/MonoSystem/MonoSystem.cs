using App.Scripts.Libs.EntryPoint.MonoInstaller;

namespace App.Scripts.Libs.Patterns.StateMachine.MonoSystem
{
    public abstract class MonoSystem : MonoInstaller, IUpdatable
    {
        public abstract void UpdateWithDT(float dt);
    }
}
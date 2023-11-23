using System.Threading.Tasks;
using App.Scripts.Architecture.Project.InputObserver;
using App.Scripts.Game.GameObjects.Ball.Pool;
using App.Scripts.Game.GameObjects.Platform;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;

namespace App.Scripts.Game.States
{
    public class StartState : GameState
    {
        private readonly InputObserver inputObserver;
        
        public StartState(GameStateMachine machine, ServiceContainer container) : base(machine, container)
        {
            inputObserver = container.GetService<InputObserver>();
            
            AddSystem<Platform>();
            AddSystem<BallPool>();
        }
        
        public override Task OnEnterState()
        {
            var platform = GetSystem<Platform>();
            var pool = GetSystem<BallPool>();

            if (pool == null) return Task.CompletedTask;
            
            platform.AddBall(pool.Get());

            return Task.CompletedTask;
        }

        public override void Update()
        {
            base.Update();

            if (inputObserver.IsUpInGame)
            {
                StateMachine.ChangeState<PlayState>();
            }
        }
        
    }
}
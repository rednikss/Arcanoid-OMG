namespace App.Scripts.Libs.StateMachine
{
    public abstract class GameState
    {
        public GameStateMachine StateMachine;

        public abstract void OnEnterState();
        
        public abstract void Update();
        
        public abstract void OnExitState();
        
    }
}
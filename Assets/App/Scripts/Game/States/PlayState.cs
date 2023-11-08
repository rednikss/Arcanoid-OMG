using App.Scripts.Libs.StateMachine;
using App.Scripts.Libs.StateMachine.MonoSystem;
using UnityEngine;

namespace App.Scripts.Game.States
{
    public class PlayState : GameState
    {
        private readonly MonoSystem[] defaultLevelSystems;
        
        //private readonly MonoSystem[] fixedLevelSystems;
        
        public PlayState(MonoSystem[] defaultSystems)
        {
            defaultLevelSystems = defaultSystems;
            //fixedLevelSystems = fixedSystems;
        }
        
        public override void OnEnterState()
        {
            
        }

        public override void Update()
        {
            foreach (var monoSystem in defaultLevelSystems)
            {
                monoSystem.UpdateWithDT(Time.deltaTime);
            }
        }

        public override void OnExitState()
        {
            
        }
    }
}
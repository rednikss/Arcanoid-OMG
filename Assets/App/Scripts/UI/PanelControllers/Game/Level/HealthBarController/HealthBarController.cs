using System.Collections.Generic;
using App.Scripts.Game.GameObjects.Ball;
using App.Scripts.Game.GameObjects.Ball.Receiver;
using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.UI.AnimatedViews.Game.HeartView;
using App.Scripts.UI.PanelControllers.Game.Level.HealthBarController.Scriptable;
using UnityEngine;

namespace App.Scripts.UI.PanelControllers.Game.Level.HealthBarController
{
    public class HealthBarController : MonoSystem
    {
        [SerializeField] private HealthScriptable scriptable;
        [SerializeField] private HeartView prefab;

        private readonly List<HeartView> hearts = new();

        private int currentHealthCount;

        private GameStateMachine machine;

        private BallReceiver receiver;
        
        public override void Init(ServiceContainer container)
        {
            Reset();

            if (receiver != container.GetService<BallReceiver>())
            {
                receiver = container.GetService<BallReceiver>();
                receiver.OnBallMiss += RemoveHeart;
            }
            
            machine = container.GetService<GameStateMachine>();
        }

        public void Reset()
        {
            currentHealthCount = scriptable.healthCount;

            for (int i = 0; i < currentHealthCount; i++)
            {
                if (hearts.Count < currentHealthCount) hearts.Add( Instantiate(prefab, transform));
                
                hearts[i].ImmediateEnable();
            }
        }

        private void RemoveHeart()
        {
            if (currentHealthCount < 1) return;
            
            var task = hearts[currentHealthCount-- - 1].Disable();

            if (currentHealthCount == 0)
            {
                machine.ChangeState<LoseState>();
            }
        }

    }
}
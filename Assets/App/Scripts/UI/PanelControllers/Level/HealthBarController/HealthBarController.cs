using System.Collections.Generic;
using App.Scripts.Game.Mechanics.Ball.Receiver;
using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.UI.AnimatedViews.Game.HeartView;
using App.Scripts.UI.PanelControllers.Level.HealthBarController.Scriptable;
using UnityEngine;

namespace App.Scripts.UI.PanelControllers.Level.HealthBarController
{
    public class HealthBarController : MonoSystem
    {
        [SerializeField] private HealthScriptable scriptable;
        [SerializeField] private HeartView prefab;

        private readonly List<HeartView> hearts = new();

        private int currentHealthCount;

        private GameStateMachine machine;
        
        public override void Init(ServiceContainer container)
        {
            currentHealthCount = scriptable.healthCount;

            bool isInstantiated = hearts.Count == currentHealthCount;
            for (int i = 0; i < currentHealthCount; i++)
            {
                if (!isInstantiated) hearts.Add( Instantiate(prefab, transform));
                
                hearts[i].ImmediateEnable();
            }

            if (isInstantiated) return;
            
            container.GetService<BallReceiver>().OnBallMiss += RemoveHeart;
            machine = container.GetService<GameStateMachine>();
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
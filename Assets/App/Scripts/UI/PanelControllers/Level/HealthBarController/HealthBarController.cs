using System;
using System.Collections.Generic;
using App.Scripts.Game.Mechanics.Ball.Pool;
using App.Scripts.Game.Mechanics.Ball.Receiver;
using App.Scripts.Game.States;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.AnimatedViews.Game.HeartView;
using App.Scripts.UI.PanelControllers.Level.HealthBarController.Scriptable;
using UnityEngine;

namespace App.Scripts.UI.PanelControllers.Level.HealthBarController
{
    public class HealthBarController : MonoInstaller
    {
        [SerializeField] private HealthScriptable scriptable;
        [SerializeField] private HeartView prefab;

        private readonly List<HeartView> hearts = new();

        private int currentHealthCount;

        private GameStateMachine machine;
        
        public override void Init(ProjectContext context)
        {
            currentHealthCount = scriptable.healthCount;

            for (int i = 0; i < currentHealthCount; i++)
            {
                hearts.Add(Instantiate(prefab, transform));
            }

            context.GetContainer().GetService<BallReceiver>().OnBallMiss += RemoveHeart;
            machine = context.GetContainer().GetService<GameStateMachine>();
        }

        public void RemoveHeart()
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
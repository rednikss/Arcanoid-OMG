using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Scripts.Game.GameObjects.Ball.Receiver;
using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.StateMachine.MonoSystem;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Fade;
using App.Scripts.UI.AnimatedViews.Game.HeartView;
using App.Scripts.UI.PanelControllers.Game.Level.HealthBarController.Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Game.Level.HealthBarController
{
    public class HealthBarController : MonoSystem
    {
        [SerializeField] private HealthScriptable scriptable;
        [SerializeField] private HeartView prefab;
        
        [SerializeField] private AnimatedCanvasFadeView glowView;
        [SerializeField] private Image glowImage;
        [SerializeField] private Color addColor;
        [SerializeField] private Color removeColor;

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
                receiver.OnBallMiss += () => AddHeart(-1);
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

        private void AddHeart(int count, bool safeRemove = false)
        {
            if (currentHealthCount < 1) return;

            currentHealthCount = Math.Clamp(currentHealthCount + count, safeRemove ? 1 : 0, scriptable.healthCount);

            for (int i = 0; i < scriptable.healthCount; i++)
            {
                var task = i < currentHealthCount ? hearts[i].Enable() : hearts[i].Disable();
            }
            
            if (currentHealthCount == 0) machine.ChangeState<LoseState>();
        }

        public async Task SafeAddHeart(int count)
        {
            AddHeart(count, true);

            var color = Math.Sign(count) > 0 ? addColor : removeColor;
            glowImage.color = color;

            await glowView.Show();
            await glowView.Hide();
        }
    }
}
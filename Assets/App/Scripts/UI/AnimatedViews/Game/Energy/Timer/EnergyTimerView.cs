using App.Scripts.Architecture.Scene.Timer;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using TMPro;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Game.Energy.Timer
{
    public class EnergyTimerView : MonoInstaller
    {
        [SerializeField] private TMP_Text timerText;

        private TimerController timerController;
        
        public override void Init(ServiceContainer container)
        {
            timerController = container.GetService<TimerController>();
        }

        private void Update()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            int minutes = (int) timerController.CurrentTime / 60;
            int seconds = (int) timerController.CurrentTime % 60;
            
            timerText.text = $"{minutes:00}:{seconds:00}";
        }

        public void SetActive(bool newState) => gameObject.SetActive(newState);
    }
}
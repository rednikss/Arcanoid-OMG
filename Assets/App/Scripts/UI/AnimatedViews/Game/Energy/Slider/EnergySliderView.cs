using App.Scripts.Game.Mechanics.Energy;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.AnimatedViews.Basic.Slider;
using App.Scripts.UI.AnimatedViews.Game.Energy.Timer;
using TMPro;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Game.Energy.Slider
{
    public class EnergySliderView : AnimatedSliderView
    {
        [SerializeField] private EnergyTimerView timerView;
        
        [SerializeField] private TMP_Text amountText;
        
        private EnergyController energyController;
        
        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            energyController = container.GetService<EnergyController>();
            
            slider.minValue = energyController.MinAmount;
            slider.maxValue = energyController.MaxAmount;

            energyController.OnAmountChanged += ChangeAmount;
            energyController.OnAmountChanged += TimerNeedCheck;
            
            TimerNeedCheck();
            ChangeAmount();
        }

        private void ChangeAmount()
        {
            SetValueAnimated(energyController.Amount);
            amountText.text = $"{energyController.Amount.ToString()}/{energyController.MaxAmount.ToString()}";
        }

        private void TimerNeedCheck()
        {
            timerView.SetActive(energyController.Amount != energyController.MaxAmount);
        }
    }
}
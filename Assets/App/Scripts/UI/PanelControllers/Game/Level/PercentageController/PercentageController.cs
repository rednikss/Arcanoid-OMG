using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.AnimatedViews.Basic.Int;
using UnityEngine;

namespace App.Scripts.UI.PanelControllers.Game.Level.PercentageController
{
    public class PercentageController : MonoInstaller
    {
        [SerializeField] private AnimatedIntView intView;
        
        public override void Init(ServiceContainer container)
        {
            intView.Init(container);
            intView.SetValue(0);
        }

        public void UpdatePercentage(int current, int min, int max)
        {
            float newValue = 1 - (float)(current - min) / (max - min);
            
            intView.SetValueAnimated((int) (newValue * 100));
        }
        
        public void Reset() => intView.SetValue(0);
    }
}
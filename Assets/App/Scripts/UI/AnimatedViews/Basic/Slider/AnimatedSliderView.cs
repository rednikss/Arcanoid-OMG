using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base.Scriptable;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Basic.Slider
{
    public class AnimatedSliderView : MonoInstaller
    {
        [SerializeField] protected UnityEngine.UI.Slider slider;
        [SerializeField] private AnimationOptionsScriptable scriptable;
        
        public override void Init(ServiceContainer container)
        {
        }
        
        public void SetValueAnimated(float newValue)
        {
            newValue = ClampValue(newValue);
            
            DOTween.To(GetValue, SetValue, newValue, scriptable.animationTime)
                .SetEase(scriptable.showEase)
                .SetLink(gameObject);
        }

        private float GetValue() => slider.value;
        
        public void SetValue(float newValue) => slider.value = newValue;
        
        public void SetMaxValue(float newValue) => slider.maxValue = newValue;
        
        public void SetMinValue(float newValue) => slider.minValue = newValue;

        private float ClampValue(float newValue) => Mathf.Clamp(newValue, slider.minValue, slider.maxValue);
        
    }
}
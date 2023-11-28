using System.Threading.Tasks;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base.Scriptable;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.UI.PanelControllers.Game.Win
{
    public class WinPanelAnimator : MonoInstaller
    {
        [SerializeField] private AnimationOptionsScriptable scriptable;
        
        [SerializeField] private RectTransform[] scaledComponents;

        [SerializeField] private RectTransform rotatingGlow;
        
        private Sequence sequence;
        
        public override void Init(ServiceContainer container)
        {
            sequence = DOTween.Sequence(gameObject).SetAutoKill(false).Pause();
            var stepTime = scriptable.animationTime / scaledComponents.Length;

            foreach (var comp in scaledComponents)
            {
                sequence.Append(comp.DOScale(Vector3.one, stepTime)
                    .SetEase(scriptable.showEase).SetAutoKill(false));
            }

            sequence.Insert(0, 
                rotatingGlow.DORotate(Vector3.forward * 360, scriptable.animationTime, RotateMode.LocalAxisAdd)
                .SetAutoKill(false));
        }
        
        public void HideContent()
        {
            foreach (var component in scaledComponents) component.localScale = Vector3.zero;
        }

        public async Task ShowContentAnimated()
        {
            sequence.Restart();
            await sequence.AsyncWaitForCompletion();
        }
    }
}
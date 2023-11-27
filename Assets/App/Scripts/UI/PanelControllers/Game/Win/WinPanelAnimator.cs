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
        
        private Tween[] tweens;
        
        private Sequence sequence;
        
        public override void Init(ServiceContainer container)
        {
            tweens = new Tween[scaledComponents.Length];
            sequence = DOTween.Sequence(gameObject).SetAutoKill(false).Pause();
            var stepTime = scriptable.animationTime / scaledComponents.Length;

            for (var i = 0; i < scaledComponents.Length; i++)
            {
                tweens[i] = scaledComponents[i].DOScale(Vector3.one, stepTime)
                    .SetEase(scriptable.showEase).SetAutoKill(false);

                sequence.Append(tweens[i]);
            }
            
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
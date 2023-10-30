using System;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base;
using DG.Tweening;

namespace App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Fade
{
    public class AnimatedCanvasFadeView : CanvasGroupView
    {
        public override void Init()
        {
            canvasGroup.interactable = false;
        }

        public override void Show(Action onComplete = null)
        {
            if (DOTween.IsTweening(canvasGroup)) canvasGroup.DOKill();

            canvasGroup.DOFade(1, scriptable.animationTime)
                .SetUpdate(true)
                .SetLink(gameObject)
                .SetEase(scriptable.showEase)
                .OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.gameObject.SetActive(true);
                })
                .OnComplete(() =>
                {
                    canvasGroup.interactable = true;
                    onComplete?.Invoke();
                });
        }

        public override void Hide(Action onComplete = null)
        {
            if (DOTween.IsTweening(canvasGroup)) canvasGroup.DOKill();
            
            canvasGroup.DOFade(0, scriptable.animationTime)
                .SetUpdate(true)
                .SetLink(gameObject)
                .SetEase(scriptable.hideEase)
                .OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.gameObject.SetActive(true);
                    canvasGroup.alpha = 1;
                })
                .OnComplete(() =>
                {
                    canvasGroup.gameObject.SetActive(false);
                    
                    canvasGroup.interactable = true;
                    onComplete?.Invoke();
                });
        }
        
        private void OnEnable() => canvasGroup.alpha = 0;
    }
}

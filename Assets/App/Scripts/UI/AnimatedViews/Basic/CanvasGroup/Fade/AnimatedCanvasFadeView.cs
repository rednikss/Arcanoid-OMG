using System.Threading.Tasks;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base;
using DG.Tweening;

namespace App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Fade
{
    public class AnimatedCanvasFadeView : AnimatedCanvasGroupView
    {
        public override void Init(ServiceContainer container)
        { }

        public override async Task Show()
        {
            if (DOTween.IsTweening(canvasGroup)) canvasGroup.DOKill();

            canvasGroup.alpha = 0;
            await canvasGroup.DOFade(1, scriptable.animationTime)
                .SetEase(scriptable.showEase)
                .SetLink(gameObject)
                .AsyncWaitForCompletion();
        }

        public override async Task Hide()
        {
            if (DOTween.IsTweening(canvasGroup)) canvasGroup.DOKill();

            canvasGroup.alpha = 1;
            await canvasGroup.DOFade(0, scriptable.animationTime)
                .SetEase(scriptable.hideEase)
                .SetLink(gameObject)
                .AsyncWaitForCompletion();
        }

        public override void ImmediateEnable()
        {
            base.ImmediateEnable();
            
            canvasGroup.alpha = 1;
        }
        
        public override void ImmediateDisable()
        {
            base.ImmediateDisable();
            
            canvasGroup.alpha = 0;
        }
    }
}

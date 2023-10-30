using System;

namespace App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base
{
    public interface ICanvasGroupView
    {
        public void Show(Action onComplete);

        public void Hide(Action onComplete);
    }
}
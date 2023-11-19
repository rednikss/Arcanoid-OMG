using System;
using System.Threading.Tasks;

namespace App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base
{
    public interface ICanvasGroupView
    {
        public Task Show();

        public Task Hide();
    }
}
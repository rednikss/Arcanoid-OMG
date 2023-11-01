using App.Scripts.UI.MVP.Start.Model;
using App.Scripts.UI.MVP.Start.Presenter;

namespace App.Scripts.UI.MVP.Start.View
{
    public interface IStartView
    {
        public void Init(IStartPresenter presenter);

        
        public void ViewData(IStartModel model);
    }
}
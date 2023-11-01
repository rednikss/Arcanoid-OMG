using App.Scripts.UI.MVP.Start.Model;
using App.Scripts.UI.MVP.Start.View;

namespace App.Scripts.UI.MVP.Start.Presenter
{
    public interface IStartPresenter
    {
        public void Init(IStartView view, IStartModel model);

        public void ChangeLocale();
        
        public void LoadScene();
    }
}
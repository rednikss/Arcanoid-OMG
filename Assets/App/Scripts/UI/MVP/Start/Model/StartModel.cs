using App.Scripts.UI.MVP.Start.View;

namespace App.Scripts.UI.MVP.Start.Model
{
    public class StartModel : IStartModel
    {
        private IStartView _view;
        
        private int _localeID;

        public void Init(IStartView view)
        {
            _view = view;
        }

        public void SetLocaleID(int newLocaleID)
        {
            _localeID = newLocaleID;
            _view.ViewData(this);
        }
    }
}
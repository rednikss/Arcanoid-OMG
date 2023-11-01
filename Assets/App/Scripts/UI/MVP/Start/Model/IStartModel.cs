using App.Scripts.UI.MVP.Start.View;

namespace App.Scripts.UI.MVP.Start.Model
{
    public interface IStartModel
    {
        public void Init(IStartView view);
        
        public void SetLocaleID(int newLocaleID);
    }
}
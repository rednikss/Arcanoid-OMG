using App.Scripts.Architecture.Patterns.ServiceLocator;
using App.Scripts.UI.MVP.Start.Model;
using App.Scripts.UI.MVP.Start.View;
using App.Scripts.Utilities.SceneLoader;
using App.Scripts.Utilities.SceneLoader.Scriptable;
using UnityEngine.Localization.Settings;

namespace App.Scripts.UI.MVP.Start.Presenter
{
    public class StartPresenter : IStartPresenter
    {
        private IStartView _view;
        private IStartModel _model;
        
        private SceneLoaderScriptable _scriptable;
        
        public void Init(IStartView view, IStartModel model)
        {
            _view = view;
            _model = model;
        }

        public void ChangeLocale()
        {
            var availableLocales = LocalizationSettings.AvailableLocales.Locales;
            var currentLocale = LocalizationSettings.SelectedLocale;
            
            var currentLocaleID = availableLocales.BinarySearch(currentLocale);
            currentLocaleID = (currentLocaleID + 1) % availableLocales.Count;
            
            LocalizationSettings.SelectedLocale = availableLocales[currentLocaleID];

            _model.SetLocaleID(currentLocaleID);
        }

        public void LoadScene()
        {
            ServiceLocator.Instance.Get<SceneLoader>().LoadScene(1);
        }
    }
}
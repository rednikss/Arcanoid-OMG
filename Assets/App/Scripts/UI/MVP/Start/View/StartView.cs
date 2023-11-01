using App.Scripts.UI.MVP.Start.Model;
using App.Scripts.UI.MVP.Start.Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.MVP.Start.View
{
    public class StartView : MonoBehaviour, IStartView
    {
        private IStartPresenter _presenter;
        
        [SerializeField] private Button playButton;
        
        [SerializeField] private Button languageButton;
        
        public void Init(IStartPresenter presenter)
        {
            _presenter = presenter;

            playButton.onClick.AddListener(() => _presenter.LoadScene());
            
            languageButton.onClick.AddListener(() => _presenter.ChangeLocale());
        }

        public void ViewData(IStartModel model)
        {
            
        }
    }
}
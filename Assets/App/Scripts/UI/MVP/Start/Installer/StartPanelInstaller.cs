using App.Scripts.Architecture.InitPoint.MonoInitializable;
using App.Scripts.UI.MVP.Start.Model;
using App.Scripts.UI.MVP.Start.Presenter;
using App.Scripts.UI.MVP.Start.View;
using UnityEngine;

namespace App.Scripts.UI.MVP.Start.Installer
{
    public class StartPanelInstaller : MonoInitializable
    {
        [SerializeField] private StartView view;
        
        public override void Init()
        {
            var newView = Instantiate(view, transform);

            var model = new StartModel();
            var presenter = new StartPresenter();
            
            newView.Init(presenter);
            presenter.Init(newView, model);
            model.Init(newView);
        }
    }
}
using App.Scripts.UI.MVP.Start.Model;
using App.Scripts.UI.MVP.Start.Presenter;
using App.Scripts.UI.MVP.Start.View;
using App.Scripts.UI.PanelManager.InstallerBase;
using UnityEngine;

namespace App.Scripts.UI.MVP.Start.Installer
{
    public class StartPanelInstaller : PanelInstaller
    {
        [SerializeField] private StartView view;
        
        public override void SetupPanel()
        {
            var model = new StartModel();
            var presenter = new StartPresenter();
            
            view.Init(presenter);
            presenter.Init(view, model);
            model.Init(view);
        }
    }
}
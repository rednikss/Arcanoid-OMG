using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.NoEnergy
{
    public class NoEnergyPanelController : LocalizedPanelController
    {
        [SerializeField] private Button okButton;
        
        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            
            okButton.onClick.AddListener(async () =>
            {
                var panelManager = container.GetService<PanelManager>();
                var newPanel = panelManager.RemoveActive();
                await newPanel.HidePanel();
            });
        }
    }
}
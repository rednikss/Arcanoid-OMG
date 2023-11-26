using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Game.Mechanics.Energy;
using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Utilities.Scene;
using App.Scripts.UI.PanelControllers.Base;
using App.Scripts.UI.PanelControllers.NoEnergy;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Game.Pause
{
    public class PausePanelController : LocalizedPanelController
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private int energyPrice;
        
        [SerializeField] private Button backButton;
        [SerializeField] private string backSceneName;
        
        [SerializeField] private Button continueButton;
        
        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            
            restartButton.onClick.AddListener(() =>
            {
                if (!container.GetService<EnergyController>().CanRemoveEnergy(energyPrice))
                {
                    var panelManager = container.GetService<PanelManager>();
                    var newPanel = panelManager.GetPanel<NoEnergyPanelController>();
                    panelManager.AddActive(newPanel);
                    var task = newPanel.ShowPanel();
                    
                    return;
                }

                container.GetService<EnergyController>().RemoveEnergy(energyPrice);
                container.GetService<GameStateMachine>().ChangeState<LoadState>();
            });
            
            continueButton.onClick.AddListener(() =>
            {
                container.GetService<GameStateMachine>().ChangeToPrevious();
            });
            
            backButton.onClick.AddListener(() =>
            {
                container.GetService<SceneLoader>().LoadScene(backSceneName);
            });
        }
    }
}
using App.Scripts.Architecture.Scene.Packs.StateController;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Game.Mechanics.Energy;
using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Utilities.Scene;
using App.Scripts.UI.PanelControllers.NoEnergy;
using UnityEngine.Advertisements;

namespace App.Scripts.UI.PanelControllers.Game.Win.AdShowListener
{
    public class NextLevelAdListener : IUnityAdsShowListener
    {
        private readonly ServiceContainer container;

        private readonly int energyPrice;
        private readonly string packSceneID;

        public NextLevelAdListener(ServiceContainer container, int energyPrice, string packSceneID)
        {
            this.container = container;
            this.energyPrice = energyPrice;
            this.packSceneID = packSceneID;
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

        public void OnUnityAdsShowStart(string placementId) { }

        public void OnUnityAdsShowClick(string placementId) { }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            var stateController = container.GetService<PackStateController>();
            if (stateController.TrySetNextLevel())
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
            }
            else
            {
                var task = container.GetService<SceneLoader>().LoadScene(packSceneID);
            }
        }
    }
}
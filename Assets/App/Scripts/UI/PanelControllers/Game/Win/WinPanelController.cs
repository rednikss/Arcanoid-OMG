using System.Threading.Tasks;
using App.Scripts.Architecture.Scene.Packs.StateController;
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

namespace App.Scripts.UI.PanelControllers.Game.Win
{
    public class WinPanelController : LocalizedPanelController
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private string packSceneID;
        
        [SerializeField] private int energyPrice;

        
        [SerializeField] private WinPanelAnimator animator;

        private PackStateController stateController;
        
        private bool isLoadNext;
        
        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            stateController = container.GetService<PackStateController>();
            
            continueButton.onClick.AddListener(() =>
            {
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
            });
        }
        
        public override async Task ShowPanel()
        {
            panelCanvasGroup.gameObject.SetActive(true);
            panelCanvasGroup.interactable = false;

            animator.HideContent();
            await canvasGroupView.Show();
            await animator.ShowContentAnimated();
            
            panelCanvasGroup.interactable = true;
        }
    }
}
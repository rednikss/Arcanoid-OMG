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
        
        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            
            continueButton.onClick.AddListener(() =>
            {
                if (container.GetService<PackStateController>().TrySetNextLevel())
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
                    container.GetService<SceneLoader>().LoadScene(packSceneID);
                }
            });
        }
    }
}
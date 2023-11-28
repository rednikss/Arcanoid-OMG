using App.Scripts.Architecture.Scene.Packs.Scriptables.Pack;
using App.Scripts.Architecture.Scene.Packs.StateController;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Game.Mechanics.Energy;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Scene;
using App.Scripts.UI.PanelControllers.NoEnergy;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.AnimatedViews.Game.PackButton
{
    public class PackButtonController : MonoInstaller
    {
        [SerializeField] private PackButtonView view;
        [SerializeField] private Button button;
        [SerializeField] private string levelSceneID;

        [SerializeField] private int energyPrice;
        
        private PackScriptable _scriptable;
        private int _completedAmount;
        
        public override void Init(ServiceContainer container)
        {
            view.Init(container);
            
            button.onClick.AddListener(() =>
            {
                if (!container.GetService<EnergyController>().CanRemoveEnergy(energyPrice))
                {
                    var panelManager = container.GetService<PanelManager>();
                    var newPanel = panelManager.GetPanel<NoEnergyPanelController>();
                    panelManager.AddActive(newPanel);
                    var task1 = newPanel.ShowPanel();
                    
                    return;
                }

                container.GetService<EnergyController>().RemoveEnergy(energyPrice);
                container.GetService<PackStateController>().SetPack(_scriptable);
                var task2 = container.GetService<SceneLoader>().LoadScene(levelSceneID);
            });
        }

        public void SetData(PackScriptable packData, int completedAmount)
        {
            _scriptable = packData;
            _completedAmount = Mathf.Max(0, completedAmount);
            view.SetCompletedLevelCount(_completedAmount);
            view.SetPackView(_scriptable);
            
            button.interactable = completedAmount >= 0;
        }
    }
}
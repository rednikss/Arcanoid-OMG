using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Packs;

namespace App.Scripts.Architecture.Scene.SceneStarter.Packs
{
    public class PacksSceneStarter : MonoInstaller
    {
        public override void Init(ServiceContainer container)
        {
            var panelManager = container.GetService<PanelManager.PanelManager>();
            var packsPanel = panelManager.GetPanel<PacksPanelController>();
            
            panelManager.AddActive(packsPanel);
            packsPanel.UpdatePackList();
            packsPanel.ShowPanelImmediately();
        }
    }
}
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelControllers.Start;

namespace App.Scripts.Architecture.Scene.SceneStarter.Start
{
    public class StartSceneStarter : MonoInstaller
    {
        public override void Init(ProjectContext context)
        {
            var panelManager = context.GetContainer().GetService<PanelManager.PanelManager>();
            
            var startPanel = panelManager.GetPanel<StartPanelController>();
            
            panelManager.AddActive(startPanel);
            startPanel.ShowPanelImmediately();
        }
    }
}
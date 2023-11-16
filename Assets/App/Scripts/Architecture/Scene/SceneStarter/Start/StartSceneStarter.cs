using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelInstallers.Start;

namespace App.Scripts.Architecture.Scene.SceneStarter.Start
{
    public class StartSceneStarter : MonoInstaller
    {
        public override void Init(ProjectContext context)
        {
            var panelManager = context.GetContainer().GetService<PanelManager.PanelManager>();
            panelManager.GetDisabledPanel<StartPanelInstaller>().gameObject.SetActive(true);
        }
    }
}
using App.Scripts.Architecture.Localization.Manager;
using App.Scripts.Architecture.Localization.Text;
using App.Scripts.Architecture.ProjectContext;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.SceneLoader;
using App.Scripts.UI.PanelInstallers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelInstallers.Start
{
    public class StartPanelInstaller : LocalizedPanelInstaller
    {
        [SerializeField] private Button playButton;
        [SerializeField] private string playSceneID;

        [SerializeField] private Button languageButton;

        public override void Init(ProjectContext context)
        {
            InitLocalizedTexts(context.GetContainer().GetService<LocaleManager>());
            
            playButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<SceneLoader>().LoadScene(playSceneID);
            });
            
            languageButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<LocaleManager>().ChangeLocale();
            });
        }
    }
}
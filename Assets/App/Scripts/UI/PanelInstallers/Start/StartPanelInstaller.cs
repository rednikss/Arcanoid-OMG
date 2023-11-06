using App.Scripts.Architecture.InitPoint.MonoInstaller;
using App.Scripts.Architecture.Localization.Manager;
using App.Scripts.Architecture.Localization.Text;
using App.Scripts.Architecture.ProjectContext;
using App.Scripts.Libs.SceneLoader;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelInstallers.Start
{
    public class StartPanelInstaller : MonoInstaller
    {
        [SerializeField] private Button playButton;
        [SerializeField] private string playSceneID;

        [SerializeField] private Button languageButton;

        [SerializeField] private LocalizedText[] localizedTexts;
        
        public override void Init(ProjectContext context)
        {
            var localeManager = context.GetServiceContainer().GetService<LocaleManager>();
            foreach (var localizedText in localizedTexts)
            {
                localizedText.Init(localeManager);
            }
            
            playButton.onClick.AddListener(() =>
            {
                context.GetServiceContainer().GetService<SceneLoader>().LoadScene(playSceneID);
            });
            
            languageButton.onClick.AddListener(() =>
            {
                context.GetServiceContainer().GetService<LocaleManager>().ChangeLocale();
            });
        }
    }
}
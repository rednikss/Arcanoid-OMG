using App.Scripts.Architecture.Project.Localization.Manager;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.Utilities.SceneLoader;
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

        private int localeID;
        
        public override void Init(ProjectContext context)
        {
            InitLocalizedTexts(context.GetContainer().GetService<LocaleManager>());
            
            playButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<SceneLoader>().LoadScene(playSceneID);
            });
            
            languageButton.onClick.AddListener(() =>
            {
                var localeManager = context.GetContainer().GetService<LocaleManager>();
                var locales = localeManager.GetAvailableLocales();
                
                localeManager.SetLocale(locales[localeID++]);

                localeID %= locales.Count;
            });
        }
    }
}
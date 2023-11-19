using App.Scripts.Architecture.Project.Localization.Manager;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.Utilities.Scene;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Start
{
    public class StartPanelController : LocalizedPanelController
    {
        [SerializeField] private Button playButton;
        [SerializeField] private string playSceneID;

        [SerializeField] private Button languageButton;

        private int localeID;
        
        public override void Init(ProjectContext context)
        {
            base.Init(context);
            
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
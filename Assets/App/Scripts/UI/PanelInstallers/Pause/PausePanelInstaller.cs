using App.Scripts.Architecture.Localization.Manager;
using App.Scripts.Architecture.ProjectContext;
using App.Scripts.Game.States;
using App.Scripts.Libs.SceneLoader;
using App.Scripts.Libs.StateMachine;
using App.Scripts.UI.PanelInstallers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelInstallers.Pause
{
    public class PausePanelInstaller : LocalizedPanelInstaller
    {
        [SerializeField] private Button restartButton;
        
        [SerializeField] private Button backButton;
        [SerializeField] private string backSceneName;
        
        [SerializeField] private Button continueButton;
        
        public override void Init(ProjectContext context)
        {
            InitLocalizedTexts(context.GetContainer().GetService<LocaleManager>());
            
            continueButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<GameStateMachine>().ChangeState<PlayState>();
            });
            
            backButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<SceneLoader>().LoadScene(backSceneName);
            });
        }
    }
}
using App.Scripts.Architecture.Project.Localization.Manager;
using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.Utilities.Scene;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Pause
{
    public class PausePanelController : LocalizedPanelController
    {
        [SerializeField] private Button restartButton;
        
        [SerializeField] private Button backButton;
        [SerializeField] private string backSceneName;
        
        [SerializeField] private Button continueButton;
        
        public override void Init(ProjectContext context)
        {
            base.Init(context);
            
            continueButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<GameStateMachine>().ChangeToPrevious();
            });
            
            backButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<SceneLoader>().LoadScene(backSceneName);
            });
        }
    }
}
using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Utilities.Scene;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Game.Pause
{
    public class PausePanelController : LocalizedPanelController
    {
        [SerializeField] private Button restartButton;
        
        [SerializeField] private Button backButton;
        [SerializeField] private string backSceneName;
        
        [SerializeField] private Button continueButton;
        
        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            
            restartButton.onClick.AddListener(() =>
            {
                container.GetService<GameStateMachine>().ChangeState<LoadState>();
            });
            
            continueButton.onClick.AddListener(() =>
            {
                container.GetService<GameStateMachine>().ChangeToPrevious();
            });
            
            backButton.onClick.AddListener(() =>
            {
                container.GetService<SceneLoader>().LoadScene(backSceneName);
            });
        }
    }
}
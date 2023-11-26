using App.Scripts.Architecture.Scene.Packs.StateController;
using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Utilities.Scene;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Game.Win
{
    public class WinPanelController : LocalizedPanelController
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private string packSceneID;

        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            
            continueButton.onClick.AddListener(() =>
            {
                if (container.GetService<PackStateController>().TrySetNextLevel())
                {
                    container.GetService<GameStateMachine>().ChangeState<LoadState>();
                }
                else
                {
                    container.GetService<SceneLoader>().LoadScene(packSceneID);
                }
            });
        }
    }
}
using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Win
{
    public class WinPanelController : LocalizedPanelController
    {
        [SerializeField] private Button continueButton;

        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            
            continueButton.onClick.AddListener(() =>
            {
                container.GetService<GameStateMachine>().ChangeState<LoadState>();
            });
        }
    }
}
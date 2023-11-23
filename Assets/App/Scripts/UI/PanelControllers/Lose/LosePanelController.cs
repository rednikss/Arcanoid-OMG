using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Lose
{
    public class LosePanelController : LocalizedPanelController
    {
        [SerializeField] private Button restartButton;

        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            
            restartButton.onClick.AddListener(() =>
            {
                container.GetService<GameStateMachine>().ChangeState<LoadState>();
            });
        }
    }
}
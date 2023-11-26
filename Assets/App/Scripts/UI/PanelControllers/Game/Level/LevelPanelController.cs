using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Game.Level
{
    public class LevelPanelController : LocalizedPanelController
    {
        [SerializeField] private Button menuButton;
        
        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            
            menuButton.onClick.AddListener(() =>
            {
                container.GetService<GameStateMachine>().ChangeState<PauseState>();
            });
        }
    }
}
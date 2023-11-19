using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Level
{
    public class LevelPanelController : LocalizedPanelController
    {
        [SerializeField] private Button menuButton;
        
        public override void Init(ProjectContext context)
        {
            base.Init(context);
            
            menuButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<GameStateMachine>().ChangeState<PauseState>();
            });
        }
    }
}
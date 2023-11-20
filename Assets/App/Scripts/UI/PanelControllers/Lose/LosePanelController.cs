using App.Scripts.Game.States;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Lose
{
    public class LosePanelController : LocalizedPanelController
    {
        [SerializeField] private Button restartButton;

        public override void Init(ProjectContext context)
        {
            base.Init(context);
            
            restartButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<GameStateMachine>().ChangeState<StartState>();
            });
            
        }
    }
}
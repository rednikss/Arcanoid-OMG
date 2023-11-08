using App.Scripts.Architecture.Localization.Manager;
using App.Scripts.Architecture.ProjectContext;
using App.Scripts.Game.States;
using App.Scripts.Libs.StateMachine;
using App.Scripts.UI.PanelInstallers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelInstallers.Level
{
    public class LevelPanelInstaller : LocalizedPanelInstaller
    {
        [SerializeField] private Button menuButton;
        
        public override void Init(ProjectContext context)
        {
            InitLocalizedTexts(context.GetContainer().GetService<LocaleManager>());
            
            menuButton.onClick.AddListener(() =>
            {
                context.GetContainer().GetService<GameStateMachine>().ChangeState<PauseState>();
            });
        }
    }
}
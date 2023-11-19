using App.Scripts.Game.States;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelControllers.Level;

namespace App.Scripts.Architecture.Scene.SceneStarter.Game
{
    public class GameSceneStarter : MonoInstaller
    {
        public override void Init(ProjectContext context)
        {
            context.GetContainer().GetService<GameStateMachine>().ChangeState<StartState>();

            var panelManager = context.GetContainer().GetService<PanelManager.PanelManager>();
            var levelPanel = panelManager.GetPanel<LevelPanelController>();
            
            panelManager.AddActive(levelPanel);
            levelPanel.ShowPanelImmediately();
        }
    }
}
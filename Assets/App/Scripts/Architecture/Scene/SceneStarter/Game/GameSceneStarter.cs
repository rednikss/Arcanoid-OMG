using App.Scripts.Game.States;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.StateMachine;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Level;

namespace App.Scripts.Architecture.Scene.SceneStarter.Game
{
    public class GameSceneStarter : MonoInstaller
    {
        public override void Init(ServiceContainer container)
        {
            var panelManager = container.GetService<PanelManager.PanelManager>();
            var levelPanel = panelManager.GetPanel<LevelPanelController>();
            
            panelManager.AddActive(levelPanel);
            levelPanel.ShowPanelImmediately();
            
            container.GetService<GameStateMachine>().ChangeState<LoadState>();
        }
    }
}
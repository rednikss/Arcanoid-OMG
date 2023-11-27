using System.Threading.Tasks;
using App.Scripts.Architecture.Scene.PanelManager;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Scene.Scriptable;
using App.Scripts.UI.PanelControllers.Transition;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Libs.Utilities.Scene
{
    public class SceneLoader : MonoInstaller
    {
        [SerializeField] private SceneLoaderScriptable scriptable;

        private PanelManager _panelManager;
        public override void Init(ServiceContainer container)
        {
            _panelManager = container.GetService<PanelManager>();
        }

        public async Task LoadScene(string scriptableID)
        {
            foreach (var sceneData in scriptable.sceneList)
            {
                if (sceneData.stringID != scriptableID) continue;

                var panel = _panelManager.GetPanel<TransitionPanelController>();
                _panelManager.AddActive(panel);
                panel.HidePanelImmediately();
                panel.transform.SetAsLastSibling();
                
                await panel.ShowPanel();
                SceneManager.LoadScene(sceneData.sceneName);
                await _panelManager.RemoveActive().HidePanel();
                
                return;
            }

            Debug.LogError($"Scene with ID = {scriptableID} is not existing!");
        }
    }
}
using App.Scripts.Architecture.InitPoint.MonoInstaller;
using App.Scripts.Architecture.ProjectContext;
using App.Scripts.Libs.SceneLoader.Scriptable;
using App.Scripts.Libs.ServiceContainer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Libs.SceneLoader
{
    public class SceneLoader : MonoInstaller
    {
        [SerializeField] private SceneLoaderScriptable scriptable;
        
        public override void Init(ProjectContext context)
        {
            context.GetServiceContainer().SetServiceSelf(this);
        }

        public void LoadScene(string scriptableID)
        {
            foreach (var sceneData in scriptable.sceneList)
            {
                if (sceneData.stringID != scriptableID) continue;
    
                SceneManager.LoadScene(sceneData.sceneName);
                return;
            }

            Debug.LogError($"Scene with ID = {scriptableID} is not existing!");
        }
    }
}
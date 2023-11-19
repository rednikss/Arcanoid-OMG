using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Utilities.Scene.Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Libs.Utilities.Scene
{
    public class SceneLoader : MonoInstaller
    {
        [SerializeField] private SceneLoaderScriptable scriptable;

        public override void Init(ProjectContext.ProjectContext context)
        {
            
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
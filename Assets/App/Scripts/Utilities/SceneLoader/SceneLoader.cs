using App.Scripts.Architecture.InitPoint.MonoInitializable;
using App.Scripts.Architecture.Patterns.ServiceLocator;
using App.Scripts.Utilities.SceneLoader.Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Utilities.SceneLoader
{
    public class SceneLoader : MonoInitializable, IService
    {
        [SerializeField] private SceneLoaderScriptable scriptable;
        
        public override void Init()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void LoadScene(int scriptableID)
        {
            for (int i = 0; i < scriptable.sceneList.Length; i++)
            {
                if (scriptable.sceneList[i].sceneID != scriptableID) continue;
    
                SceneManager.LoadScene(scriptable.sceneList[scriptableID].sceneName);
                return;
            }
            
            Debug.LogError($"Scene with ID = {scriptableID} is not existing!");
        }
    }
}
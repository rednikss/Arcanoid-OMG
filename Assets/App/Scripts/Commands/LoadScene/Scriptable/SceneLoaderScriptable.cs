using UnityEngine;

namespace App.Scripts.Commands.LoadScene.Scriptable
{
    [CreateAssetMenu(fileName = "Scene List", menuName = "Scriptable Object/Base/Scene Config", order = 0)]
    public class SceneLoaderScriptable : ScriptableObject
    {
        [SerializeField] 
        public string menuSceneName;
        
        [SerializeField] 
        public string packSceneName;
        
        [SerializeField] 
        public string gameSceneName;
        
        [SerializeField] 
        public string winSceneName;
    }
}
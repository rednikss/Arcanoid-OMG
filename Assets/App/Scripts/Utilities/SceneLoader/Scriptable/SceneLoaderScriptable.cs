using System;
using UnityEngine;

namespace App.Scripts.Utilities.SceneLoader.Scriptable
{
    [CreateAssetMenu(fileName = "Scene List", menuName = "Scriptable Object/Base/Scene Config", order = 0)]
    public class SceneLoaderScriptable : ScriptableObject
    {
        [Serializable]
        public class SceneData
        {
            public int sceneID;
            public string sceneName;
        }

        public SceneData[] sceneList;
    }
}
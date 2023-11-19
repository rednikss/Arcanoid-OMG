using System;
using UnityEngine;

namespace App.Scripts.Libs.Utilities.Scene.Scriptable
{
    [CreateAssetMenu(fileName = "Scene List", menuName = "Scriptable Object/Base/Scene Config", order = 0)]
    public class SceneLoaderScriptable : ScriptableObject
    {
        [Serializable]
        public class SceneData
        {
            public string stringID;
            public string sceneName;
        }

        public SceneData[] sceneList;
    }
}
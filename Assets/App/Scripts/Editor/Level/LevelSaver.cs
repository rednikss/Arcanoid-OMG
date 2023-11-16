using System.IO;
using App.Scripts.Game.LevelManager;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace App.Scripts.Editor.Level
{
    [CustomEditor(typeof(LevelLoader), true)]
    public class LevelSaver : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var component = (LevelLoader) target;
            
            VisualElement myInspector = new VisualElement();

            InspectorElement.FillDefaultInspector(myInspector, serializedObject, this);

            var pathText = new TextField("Level File Full Name");

            var button = new Button(() =>
            {
                component.SaveCurrentAsLevel(Path.Combine(Application.dataPath, pathText.value));
            });
            
            button.text = "Save Current Tilemap As A New Level";
            myInspector.Add(button);
            
            myInspector.Add(pathText);
            
            return myInspector;
        }
    }
}
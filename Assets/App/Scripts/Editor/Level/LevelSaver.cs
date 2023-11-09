using App.Scripts.Game.LevelLoader;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace App.Scripts.Editor.Level
{
    [CustomEditor(typeof(LevelLoader), true)]
    public class LevelSaver : UnityEditor.Editor
    {
        private string currentPath;
        
        public override VisualElement CreateInspectorGUI()
        {
            var component = (LevelLoader) target;
            
            VisualElement myInspector = new VisualElement();

            InspectorElement.FillDefaultInspector(myInspector, serializedObject, this);

            var pathText = new TextField("Level File Path");
            pathText.value = currentPath;
            
            var button = new Button(() =>
            {
                currentPath = pathText.value;
                component.SaveCurrentAsLevel(pathText.value);
            });
            
            button.text = "Save Current Tilemap As A New Level";
            myInspector.Add(button);
            
            myInspector.Add(pathText);
            
            return myInspector;
        }
    }
}
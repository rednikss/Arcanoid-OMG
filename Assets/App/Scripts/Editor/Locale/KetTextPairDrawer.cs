using App.Scripts.Architecture.Localization.Scriptable.Locale;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Editor.Locale
{
    [CustomPropertyDrawer(typeof(LocaleScriptable.KeyTextPair))]
    public class KetTextPairDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {  
            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;

            var positionSizeX = position.size.x;
            
            position.size = new Vector2(positionSizeX * 0.34f, position.size.y);
            
            EditorGUI.PropertyField(position, property.FindPropertyRelative("key"), GUIContent.none);

            position.position += new Vector2(position.size.x, 0);
            position.size = new Vector2(positionSizeX * 0.66f, position.size.y);
            
            EditorGUI.PropertyField(position, property.FindPropertyRelative("text"), GUIContent.none);
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
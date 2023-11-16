using UnityEngine;

namespace App.Scripts.Architecture.Project.Localization.Scriptable.AvailableLocales
{
    [CreateAssetMenu(
        fileName = "Available Locales", 
        menuName = "Scriptable Object/Locales/Available Locales Config", 
        order = 0)]
    public class AvailableLocalesScriptable : ScriptableObject
    {
        public string defaultLocaleName;
        
        public string[] localesNames;
    }
}
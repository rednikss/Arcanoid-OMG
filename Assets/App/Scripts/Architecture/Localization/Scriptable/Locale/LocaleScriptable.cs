using System;
using UnityEngine;

namespace App.Scripts.Architecture.Localization.Scriptable.Locale
{
    [CreateAssetMenu(fileName = "New Locale", menuName = "Scriptable Object/Localization", order = 0)]
    public class LocaleScriptable : ScriptableObject
    {
        public string localeName;

        public KeyTextPair[] pairs;

        [Serializable]
        public class KeyTextPair
        {
            public string key;
            public string text;
        }
    }
}
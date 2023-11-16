using System;
using System.IO;
using App.Scripts.Architecture.Project.Localization.Scriptable.Locale;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Editor.Locale
{
    public class LocaleCreator : EditorWindow
    {
        private TextAsset keysList;
        private string keysSeparator = Environment.NewLine;

        private TextAsset wordsList;
        private string wordsSeparator = Environment.NewLine;
        
        private LocaleScriptable currentTarget;

        [MenuItem("Window/Locale Creator")]
        public static void ShowWindow()
        {
            GetWindow(typeof(LocaleCreator));
        }
        
        private void OnGUI()
        {
            EditorGUILayout.Space(10f);
            keysList = (TextAsset) EditorGUILayout.ObjectField("Keys List", 
                keysList, typeof(TextAsset), false);
            
            keysSeparator = EditorGUILayout.TextField("Keys Separator", keysSeparator);
            EditorGUILayout.Space(20f);
            
            wordsList = (TextAsset) EditorGUILayout.ObjectField("Words List", 
                wordsList, typeof(TextAsset), false);
            
            wordsSeparator = EditorGUILayout.TextField("Words Separator", wordsSeparator);
            EditorGUILayout.Space(20f);
            
            currentTarget = (LocaleScriptable) EditorGUILayout.ObjectField("Target", 
                currentTarget, typeof(LocaleScriptable), false);
            EditorGUILayout.Space(20f);


            if (GUILayout.Button("Create new Locale"))
            {
                CreateLocale();
            }
            
            if (GUILayout.Button("Update Target Locale"))
            {
                UpdateTarget();
            }
        }

        
        private void CreateLocale()
        {
            currentTarget = CreateInstance<LocaleScriptable>();
            
            UpdateTarget();

            string filePath = Path.Combine("Assets", "Resources", "Locales", $"{wordsList.name} Locale.asset");
            AssetDatabase.CreateAsset(currentTarget, filePath);
            AssetDatabase.SaveAssets();
        }

        private void UpdateTarget()
        {
            currentTarget.localeName = wordsList.name;
            InitializeKeys(currentTarget);
            InitializeWords(currentTarget);
        }
        
        private void InitializeKeys(LocaleScriptable target)
        {
            var keys = keysList.text.Split(keysSeparator);

            target.pairs = new LocaleScriptable.KeyTextPair[keys.Length];
            
            for (int i = 0; i < keys.Length; i++)
            {
                target.pairs[i] = new LocaleScriptable.KeyTextPair();
                target.pairs[i].key = keys[i];
            }
        }
        
        private void InitializeWords(LocaleScriptable target)
        {
            var words = wordsList.text.Split(wordsSeparator);

            for (int i = 0; i < words.Length; i++)
            {
                target.pairs[i].text = words[i];
            }
        }
    }
}
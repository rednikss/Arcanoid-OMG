using System;
using System.Collections.Generic;
using System.IO;
using App.Scripts.Architecture.Project.Localization.Scriptable.AvailableLocales;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.Utilities.Data.DataProvider;
using UnityEngine;

namespace App.Scripts.Architecture.Project.Localization.Manager
{
    public class LocaleManager : MonoInstaller
    {
        [SerializeField] private AvailableLocalesScriptable scriptable;

        [SerializeField] private string localesPath;
        
        private IDataProvider _provider;

        private LocaleInfo _current;

        public event Action OnLocaleChanged;
        
        public override void Init(ProjectContext context)
        {
            _provider = context.GetContainer().GetService<IDataProvider>();
            _provider.LoadData(out _current);

            SetLocale(_current.Name ?? scriptable.defaultLocaleName);
        }

        public List<string> GetAvailableLocales()
        {
            List<string> locales = new(scriptable.localesNames);

            locales.Remove(_current.Name);

            return locales;
        }
        
        public void SetLocale(string localeName)
        {
            _current.Name = localeName;
            _current.Words = LoadLocaleWords(localeName);
            
            _provider.SaveData(_current);
            
            OnLocaleChanged?.Invoke();
        }
        
        private Dictionary<string, string> LoadLocaleWords(string localeName)
        {
            string path = Path.Combine(localesPath, localeName);
            var localeData = Resources.Load<TextAsset>(path);

            Dictionary<string, string> words = new();
            
            var pairs = localeData.text.Split(Environment.NewLine);

            foreach (var pair in pairs)
            {
                var data = pair.Split(';', 2);
                words[data[0]] = data[1];
            }

            return words;
        }
        
        public string GetLocalizedText(string key) => _current.Words[key];
        
        private class LocaleInfo
        {
            public string Name;
            public Dictionary<string, string> Words;

            public LocaleInfo()
            {
                Name = null;
                Words = new();
            }
        }
    }
}
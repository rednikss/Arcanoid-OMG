using System;
using System.Collections.Generic;
using App.Scripts.Architecture.Project.Localization.Scriptable.AvailableLocales;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Data.DataProvider;
using UnityEngine;

namespace App.Scripts.Architecture.Project.Localization.Manager
{
    public class LocaleManager : MonoInstaller
    {
        [SerializeField] private AvailableLocalesScriptable scriptable;

        [SerializeField] private string localesPath;
        
        [SerializeField] private string currentLocalePath;
        
        private IDataProvider _fileProvider;
        
        private IDataProvider _resourcesProvider;

        private LocaleInfo _current;
        private readonly Dictionary<string, string> currentDictionary = new();
        
        public event Action OnLocaleChanged;
        
        public override void Init(ServiceContainer container)
        {
            _fileProvider = container.GetService<FileDataProvider>();
            _resourcesProvider = container.GetService<ResourcesTextDataProvider>();
            
            _current = _fileProvider.LoadData<LocaleInfo>(nameof(LocaleInfo), currentLocalePath);
            foreach (var pair in _current.Words) currentDictionary[pair.Key] = pair.Value;
            
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
            _current = _resourcesProvider.LoadData<LocaleInfo>(localeName, localesPath);
            foreach (var pair in _current.Words) currentDictionary[pair.Key] = pair.Value;

            _fileProvider.SaveData(_current, nameof(LocaleInfo), currentLocalePath);
            
            OnLocaleChanged?.Invoke();
        }
        
        public string GetLocalizedText(string key) => currentDictionary[key];
        
        [Serializable]
        private class LocaleInfo
        {
            public string Name;

            public List<StringPair> Words;

            public LocaleInfo()
            {
                Name = null;
                Words = new();
            }
        }

        [Serializable]
        private class StringPair
        {
            public string Key;
            public string Value;

            public StringPair(string key, string value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
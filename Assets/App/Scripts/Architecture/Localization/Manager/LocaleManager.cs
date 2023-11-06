using System;
using System.Collections.Generic;
using System.IO;
using App.Scripts.Architecture.Data.DataLoader;
using App.Scripts.Architecture.InitPoint.MonoInstaller;
using App.Scripts.Architecture.Localization.Scriptable.AvailableLocales;
using App.Scripts.Architecture.Localization.Scriptable.Locale;
using App.Scripts.Libs.ServiceContainer;
using UnityEngine;

namespace App.Scripts.Architecture.Localization.Manager
{
    public class LocaleManager : MonoInstaller
    {
        [SerializeField] private AvailableLocalesScriptable scriptable;

        [SerializeField] private string localesPath;
        
        private IDataProvider _provider;

        private LocaleInfo _current;

        public event Action OnLocaleChanged;
        
        public override void Init(ProjectContext.ProjectContext context)
        {
            context.GetServiceContainer().SetServiceSelf(this);
            
            _provider = context.GetServiceContainer().GetService<IDataProvider>();
            _provider.LoadData(out _current);

            SetLocale(_current.ID);
            
            OnLocaleChanged?.Invoke();
        }

        
        public void ChangeLocale() => SetLocale((_current.ID + 1) % scriptable.localesNames.Length);

        private void SetLocale(string localeName)
        {
            _current.Name = localeName;
            for (int i = 0; i < scriptable.localesNames.Length; i++)
            {
                if (localeName != scriptable.localesNames[i]) continue;
                
                _current.ID = i;
                break;
            }
            
            LoadLocaleWords();
            OnLocaleChanged?.Invoke();
        }
        
        private void SetLocale(int localeID)
        {
            _current.ID = localeID;
            _current.Name = scriptable.localesNames[localeID];
            
            LoadLocaleWords();
            OnLocaleChanged?.Invoke();
        }

        private void LoadLocaleWords()
        {
            string path = Path.Combine(localesPath, _current.Name);
            var localeData = Resources.Load<LocaleScriptable>(path);

            _current.Words.Clear();
            foreach (var keyTextPair in localeData.pairs)
            {
                _current.Words.Add(keyTextPair.key, keyTextPair.text);
            }
            
            _provider.SaveData(_current);
        }
        

        public string GetLocalizedText(string key) => _current.Words[key];
        
        private class LocaleInfo
        {
            public string Name;
            public int ID;
            public Dictionary<string, string> Words;

            public LocaleInfo()
            {
                ID = 0;
                Name = String.Empty;
                Words = new();
            }
        }
    }
}
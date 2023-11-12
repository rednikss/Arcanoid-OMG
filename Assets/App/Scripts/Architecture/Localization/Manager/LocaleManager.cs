using System;
using System.Collections.Generic;
using System.IO;
using App.Scripts.Architecture.Localization.Scriptable.AvailableLocales;
using App.Scripts.Libs.Data.DataProvider;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
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
        
        public override void Init(ProjectContext context)
        {
            _provider = context.GetContainer().GetService<IDataProvider>();
            _provider.LoadData(out _current);

            SetLocale(_current.ID);
            
            context.GetContainer().SetServiceSelf(this);
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
            _provider.SaveData(_current);
            
            OnLocaleChanged?.Invoke();
        }
        
        private void SetLocale(int localeID)
        {
            _current.ID = localeID;
            _current.Name = scriptable.localesNames[localeID];
            
            LoadLocaleWords();
            _provider.SaveData(_current);
            
            OnLocaleChanged?.Invoke();
        }

        private void LoadLocaleWords()
        {
            string path = Path.Combine(localesPath, _current.Name);
            var localeData = Resources.Load<TextAsset>(path);

            var pairs = localeData.text.Split(Environment.NewLine);

            foreach (var pair in pairs)
            {
                var data = pair.Split(';', 2);
                _current.Words[data[0]] = data[1];
            }

        }
        

        public string GetLocalizedText(string key) => _current.Words[key];
        
        private class LocaleInfo
        {
            public string Name;
            public int ID;
            public readonly Dictionary<string, string> Words;

            public LocaleInfo()
            {
                ID = 0;
                Name = String.Empty;
                Words = new();
            }
        }
    }
}
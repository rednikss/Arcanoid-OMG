using System;
using App.Scripts.Architecture.Localization.Manager;
using TMPro;
using UnityEngine;

namespace App.Scripts.Architecture.Localization.Text
{
    [Serializable]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        
        [SerializeField] private string key;

        private LocaleManager _manager;
        public void Init(LocaleManager manager)
        {
            _manager = manager;
            _manager.OnLocaleChanged += SetLocalizedText;
            
            SetLocalizedText();
        }

        private void SetLocalizedText()
        {
            tmpText.text = _manager.GetLocalizedText(key);
        }
    }
}
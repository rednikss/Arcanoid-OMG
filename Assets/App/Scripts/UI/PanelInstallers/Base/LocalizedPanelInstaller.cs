using System;
using App.Scripts.Architecture.Project.Localization.Manager;
using App.Scripts.Architecture.Project.Localization.Text;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.UI.PanelInstallers.Base
{
    [Serializable]
    public abstract class LocalizedPanelInstaller : MonoInstaller
    {
        [SerializeField] protected LocalizedText[] localizedTexts;

        protected void InitLocalizedTexts(LocaleManager localeManager)
        {
            foreach (var localizedText in localizedTexts)
            {
                localizedText.Init(localeManager);
            }
        }
    }
}
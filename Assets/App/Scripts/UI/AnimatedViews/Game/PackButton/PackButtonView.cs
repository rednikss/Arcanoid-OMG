using App.Scripts.Architecture.Project.Localization.Manager;
using App.Scripts.Architecture.Project.Localization.Text;
using App.Scripts.Architecture.Scene.Packs.Scriptables.Pack;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.AnimatedViews.Game.PackButton
{
    public class PackButtonView : MonoInstaller
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Graphic[] mainColorElements;
        [SerializeField] private Graphic[] secondaryColorElements;
        [SerializeField] private TMP_Text[] mainTextElements;
        [SerializeField] private TMP_Text[] secondaryTextElements;
        
        [SerializeField] private TMP_Text counterText;
        
        [SerializeField] private LocalizedText packNameText;
        [SerializeField] private LocalizedText[] texts;

        private int allLevelCount;
        private int completedLevelCount;
        
        public override void Init(ServiceContainer container)
        {
            var localeManager = container.GetService<LocaleManager>();
            foreach(var text in texts) text.Init(localeManager);
            packNameText.Init(localeManager);
        }

        public void SetPackView(PackScriptable pack)
        {
            allLevelCount = pack.levelCount;
            counterText.text = $"{completedLevelCount.ToString()}/{allLevelCount.ToString()}";
            
            foreach (var graphic in mainColorElements) graphic.color = pack.mainViewColor;
            foreach (var graphic in secondaryColorElements) graphic.color = pack.secondaryViewColor;
            foreach (var text in mainTextElements)
            {
                text.fontMaterial = pack.fontMaterial;
                text.colorGradientPreset = pack.mainTextGradient;
            }

            foreach (var text in secondaryTextElements)
            {
                text.fontMaterial = pack.fontMaterial;
                text.colorGradientPreset = pack.secondaryTextGradient;
            }

            iconImage.sprite = pack.icon;
            packNameText.Key = pack.packNameKey;
        }

        public void SetCompletedLevelCount(int count)
        {
            completedLevelCount = count;
            counterText.text = $"{completedLevelCount.ToString()}/{allLevelCount.ToString()}";
        }
    }
}
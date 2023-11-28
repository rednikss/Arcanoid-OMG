using App.Scripts.Architecture.Project.Localization.Text;
using App.Scripts.Architecture.Scene.Packs.StateController;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.AnimatedViews.Game.PackInfo
{
    public class PackInfoView : MonoInstaller
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private LocalizedText packNameText;
        [SerializeField] private TMP_Text counterText;
        
        private PackStateController stateController;
        
        public override void Init(ServiceContainer container)
        {
            stateController = container.GetService<PackStateController>();
            
            stateController.OnCurrentPackChanged += UpdateView;
            UpdateView();
        }

        private void UpdateView()
        {
            var pack = stateController.CurrentPack;
            
            iconImage.sprite = pack.icon;
            if (packNameText != null) packNameText.Key = pack.packNameKey;
            counterText.text = $"{stateController.GetCompletedAmount(pack).ToString()}/{pack.levelCount.ToString()}";
        }
    }
}
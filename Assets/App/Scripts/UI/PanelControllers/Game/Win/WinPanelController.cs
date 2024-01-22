using System.Threading.Tasks;
using App.Scripts.Architecture.Scene.Packs.StateController;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.AdPlayer.InterstitialAdProvider;
using App.Scripts.UI.PanelControllers.Base;
using App.Scripts.UI.PanelControllers.Game.Win.AdShowListener;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Game.Win
{
    public class WinPanelController : LocalizedPanelController
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private string packSceneID;
        
        [SerializeField] private int energyPrice;

        
        [SerializeField] private WinPanelAnimator animator;

        private PackStateController stateController;

        private UnityAdsProvider adsProvider;
        
        private bool isLoadNext;
        
        public override void Init(ServiceContainer container)
        {
            base.Init(container);
            adsProvider = container.GetService<UnityAdsProvider>();
            
            continueButton.onClick.AddListener(() =>
            {
                adsProvider.ShowAd(new NextLevelAdListener(container,energyPrice, packSceneID));
            });
        }
        
        public override async Task ShowPanel()
        {
            panelCanvasGroup.gameObject.SetActive(true);
            panelCanvasGroup.interactable = false;

            adsProvider.LoadAd();
            animator.HideContent();
            await canvasGroupView.Show();
            await animator.ShowContentAnimated();
            
            panelCanvasGroup.interactable = true;
        }
    }
}
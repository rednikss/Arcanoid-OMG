using System;
using System.Threading.Tasks;
using App.Scripts.Architecture.Project.Localization.Manager;
using App.Scripts.Architecture.Project.Localization.Text;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base;
using UnityEngine;

namespace App.Scripts.UI.PanelControllers.Base
{
    [Serializable]
    public abstract class LocalizedPanelController : MonoInstaller
    {
        [SerializeField] protected CanvasGroup panelCanvasGroup;
        [SerializeField] protected AnimatedCanvasGroupView canvasGroupView;
        [SerializeField] protected LocalizedText[] localizedTexts;

        public override void Init(ServiceContainer container)
        {
            InitLocalizedTexts(container.GetService<LocaleManager>());
        }
        
        protected void InitLocalizedTexts(LocaleManager localeManager)
        {
            foreach (var localizedText in localizedTexts)
            {
                localizedText.Init(localeManager);
            }
        }

        public void SetInteractive(bool newState)
        {
            panelCanvasGroup.interactable = newState;
        }

        public async Task ShowPanel()
        {
            panelCanvasGroup.gameObject.SetActive(true);
            panelCanvasGroup.interactable = false;
            
            await canvasGroupView.Show();
            
            panelCanvasGroup.interactable = true;
        }

        public async Task HidePanel()
        {
            panelCanvasGroup.gameObject.SetActive(true);
            panelCanvasGroup.interactable = false;
            
            await canvasGroupView.Hide();
            
            panelCanvasGroup.gameObject.SetActive(false);
        } 
        
        
        public void ShowPanelImmediately() => canvasGroupView.ImmediateEnable();

        public void HidePanelImmediately() => canvasGroupView.ImmediateDisable();
    }
}
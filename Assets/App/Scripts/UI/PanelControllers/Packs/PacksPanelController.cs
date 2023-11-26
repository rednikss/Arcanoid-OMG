using System.Collections.Generic;
using App.Scripts.Architecture.Scene.Packs.Scriptables.PackList;
using App.Scripts.Architecture.Scene.Packs.StateController;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Scene;
using App.Scripts.UI.AnimatedViews.Game.PackButton;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.PanelControllers.Packs
{
    public class PacksPanelController : LocalizedPanelController
    {
        [SerializeField] private Button backButton;
        [SerializeField] private string startSceneID;
        
        [SerializeField] private PackList packList;
        [SerializeField] private PackButtonController packButton;
        [SerializeField] private Transform content;

        private PackStateController stateController;

        private readonly List<PackButtonController> buttons = new();

        private ServiceContainer _container;
        
        public override void Init(ServiceContainer container)
        {
            _container = container;
            base.Init(container);
            
            backButton.onClick.AddListener(() =>
            {
                container.GetService<SceneLoader>().LoadScene(startSceneID);
            });
            
            stateController = container.GetService<PackStateController>();
            var infoCount = stateController.Count;
            
            for (var i = 0; i < infoCount; i++)
            {
                var newButton = Instantiate(packButton, content);
                
                buttons.Add(newButton);
            }
            
            UpdatePackList();
        }

        public void UpdatePackList()
        {
            stateController.UpdateState();
            var infoCount = stateController.Count;
            
            for (var i = 0; i < infoCount; i++)
            {
                var curInfo = stateController.GetCurrentLevel(i);
                
                buttons[i].Init(_container);
                buttons[i].SetData(curInfo > 0 ? packList.packs[i] : packList.blockedPack, curInfo);
            }
        }
    }
}
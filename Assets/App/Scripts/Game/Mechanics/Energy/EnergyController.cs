using System;
using App.Scripts.Architecture.Scene.Timer;
using App.Scripts.Game.Mechanics.Energy.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Data.DataProvider;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Energy
{
    public class EnergyController : MonoInstaller
    {
        [SerializeField] private string filePath;
        
        [SerializeField] private EnergyScriptable scriptable;
        public int MaxAmount => scriptable.maxAmount;
        public int MinAmount => scriptable.minAmount;
        
        private EnergyStateInfo _currentInfo;
        private FileDataProvider fileProvider;

        private TimerController timerController;
        
        public event Action OnAmountChanged;
        
        public int Amount
        {
            get => _currentInfo.CurrentAmount;

            private set
            {
                if (_currentInfo.CurrentAmount == MaxAmount && value != MaxAmount)
                    timerController.OnTimerEnd += AddEnergy;
                
                if (_currentInfo.CurrentAmount != MaxAmount && value == MaxAmount)
                    timerController.OnTimerEnd -= AddEnergy;
                
                _currentInfo.CurrentAmount = value;
                OnAmountChanged?.Invoke();
            }
            
        }
        
        public override void Init(ServiceContainer container)
        {
            timerController = container.GetService<TimerController>();
            timerController.OnTimerEnd += AddEnergy;
            
            fileProvider = container.GetService<FileDataProvider>();
            _currentInfo = fileProvider.LoadData<EnergyStateInfo>(nameof(EnergyStateInfo), filePath);

            if (_currentInfo.CurrentAmount >= 0) return;
            
            Amount = scriptable.defaultAmount;
            fileProvider.SaveData(_currentInfo, nameof(EnergyStateInfo), filePath);
        }

        public bool CanRemoveEnergy(int amount) => _currentInfo.CurrentAmount >= amount;
        
        public void RemoveEnergy(int amount)
        {
            Amount -= amount;
            
            fileProvider.SaveData(_currentInfo, nameof(EnergyStateInfo), filePath);
        }
        
        public void AddEnergy(int amount)
        {
            _currentInfo.CurrentAmount += amount * scriptable.increaseAmount;
            Amount = Mathf.Min(_currentInfo.CurrentAmount, scriptable.maxAmount);

            fileProvider.SaveData(_currentInfo, nameof(EnergyStateInfo), filePath);
        }
    }
}
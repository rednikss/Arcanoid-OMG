using System;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Data.DataProvider;
using UnityEngine;

namespace App.Scripts.Architecture.Scene.Timer
{
    public class TimerController : MonoInstaller
    {
        [SerializeField] private uint timerSeconds;

        [SerializeField] private string filePath;

        public float CurrentTime { get; private set; }

        private TimerStateInfo currentInfo;

        private FileDataProvider fileProvider;

        public event Action<int> OnTimerEnd;

        private bool isTicking;
        
        public override void Init(ServiceContainer container)
        {
            fileProvider = container.GetService<FileDataProvider>();
            currentInfo = fileProvider.LoadData<TimerStateInfo>(nameof(TimerStateInfo), filePath);

            if (currentInfo.TimerSecondsTime == 0) SaveState();
            
            LoadState();
            
            isTicking = true;
        }

        private void Update()
        {
            if (!isTicking) return;
            
            CurrentTime -= Time.deltaTime;
            if (CurrentTime > 0) return;
            
            CurrentTime = timerSeconds;
            OnTimerEnd?.Invoke(1);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            isTicking = hasFocus;
            if (hasFocus) LoadState();
            else SaveState();
        }

        private void LoadState()
        {
            currentInfo = fileProvider.LoadData<TimerStateInfo>(nameof(TimerStateInfo), filePath);

            double timeDelta = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
            timeDelta -= currentInfo.LastTotalSeconds;

            CurrentTime = currentInfo.TimerSecondsTime - (float)(timeDelta % timerSeconds);
            if (CurrentTime < 0) CurrentTime += timerSeconds;
            
            OnTimerEnd?.Invoke(Mathf.FloorToInt((float)(timeDelta / timerSeconds)));
        }
        
        private void SaveState()
        {
            currentInfo.TimerSecondsTime = CurrentTime; 
            currentInfo.LastTotalSeconds = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
            
            fileProvider.SaveData(currentInfo, nameof(TimerStateInfo), filePath);
        }
    }
}
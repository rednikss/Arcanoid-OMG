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
            CurrentTime = timerSeconds;
            
            fileProvider = container.GetService<FileDataProvider>();
            currentInfo = fileProvider.LoadData<TimerStateInfo>(nameof(TimerStateInfo), filePath);

            if (currentInfo.TimerSecondsTime == 0) SaveState();
            
            double timeDelta = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds - currentInfo.LastTotalSeconds;
            float timerEndCount = (float) timeDelta / currentInfo.TimerSecondsTime;
            
            OnTimerEnd?.Invoke(Mathf.FloorToInt(timerEndCount));
            
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
            if (hasFocus)
            {
                LoadState();
                
                double timeDelta = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds - currentInfo.LastTotalSeconds;
                float timerEndCount = (float) timeDelta / currentInfo.TimerSecondsTime;
            
                OnTimerEnd?.Invoke(Mathf.FloorToInt(timerEndCount));
            }
            else SaveState();
        }

        private void LoadState()
        {
            currentInfo = fileProvider.LoadData<TimerStateInfo>(nameof(TimerStateInfo), filePath);
        }
        
        private void SaveState()
        {
            currentInfo.TimerSecondsTime = timerSeconds;
            currentInfo.LastTotalSeconds = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
            fileProvider.SaveData(currentInfo, nameof(TimerStateInfo), filePath);
        }
    }
}
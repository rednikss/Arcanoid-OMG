using System;
using App.Scripts.Architecture.Scene.Packs.Scriptables.Pack;
using App.Scripts.Architecture.Scene.Packs.Scriptables.PackList;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Data.DataProvider;
using UnityEngine;

namespace App.Scripts.Architecture.Scene.Packs.StateController
{
    public class PackStateController : MonoInstaller
    {
        [SerializeField] private PackList packList;
        [SerializeField] private string filePath;

        public event Action OnCurrentPackChanged;
        
        private PackScriptable _currentPack;
        public PackScriptable CurrentPack 
        {
            get => _currentPack;
            private set
            {
                _currentPack = value;
                OnCurrentPackChanged?.Invoke();
            }
            
        }
        
        public int Count { get; private set; }
        
        private FileDataProvider fileProvider;
        private PackStateInfo currentInfo;
        
        public override void Init(ServiceContainer container)
        {
            fileProvider = container.GetService<FileDataProvider>();
            currentInfo = fileProvider.LoadData<PackStateInfo>(nameof(PackStateInfo), filePath);

            if (currentInfo.LevelToLoad == 0)
            {
                currentInfo = CreateStartPackState();
                SaveState();
            }

            Count = currentInfo.CurrentLevel.Length;
            CurrentPack = GetPackByLevel(currentInfo.LevelToLoad);
        }

        public int GetCurrentLevel(int id) => currentInfo.CurrentLevel[id];
        
        public int GetCompletedAmount(int id) => currentInfo.CompletedAmount[id];

        public int GetCurrentLevel(PackScriptable pack) => currentInfo.CurrentLevel[GetPackID(pack)];

        public int GetCompletedAmount(PackScriptable pack) => currentInfo.CompletedAmount[GetPackID(pack)];
        
        public void SetPack(PackScriptable pack)
        {
            currentInfo.LevelToLoad = GetLevelByPack(pack);
            CurrentPack = pack;
            SaveState();
        }

        public bool TrySetNextLevel()
        {
            var newPack = GetPackByLevel(++currentInfo.LevelToLoad);
            
            if (CurrentPack.Equals(newPack))
            {
                var id = GetPackID(CurrentPack);
                currentInfo.CompletedAmount[id] = 
                    Math.Max(currentInfo.CurrentLevel[id], currentInfo.CompletedAmount[id]);
                currentInfo.CurrentLevel[id]++;
                OnCurrentPackChanged?.Invoke();
                SaveState();
                return true;
            }

            if (newPack == null) currentInfo.LevelToLoad = 1;
            
            for (int i = 0; i < Count; i++)
            {
                if (!packList.packs[i].Equals(CurrentPack)) continue;

                currentInfo.CurrentLevel[i] = 1;
                currentInfo.CompletedAmount[i] = CurrentPack.levelCount;
                    
                if (i + 1 < Count) currentInfo.CompletedAmount[i + 1] = Math.Max(currentInfo.CompletedAmount[i + 1], 0);
            }

            SaveState();
            return false;
        }

        private int GetPackID(PackScriptable pack)
        {
            for (var i = 0; i < packList.packs.Length; i++)
            {
                var packScriptable = packList.packs[i];
                if (packScriptable.Equals(pack)) return i;
            }

            return -1;
        }
        private int GetLevelByPack(PackScriptable currentPack)
        {
            int curLevel = 0;
            for (var i = 0; i < Count; i++)
            {
                var pack = packList.packs[i];
                if (pack.Equals(currentPack))
                {
                    curLevel += currentInfo.CurrentLevel[i];
                    break;
                }

                curLevel += pack.levelCount;
            }

            return curLevel;
        }
        
        private PackScriptable GetPackByLevel(int levelId)
        {
            int curLevel = 0;
            foreach (var pack in packList.packs)
            {
                curLevel += pack.levelCount;
                if (curLevel < levelId) continue;
                return pack;
            }

            return null;
        }
        
        private PackStateInfo CreateStartPackState()
        {
            PackStateInfo startInfo = new()
            {
                CurrentLevel = new int[packList.packs.Length],
                CompletedAmount = new int[packList.packs.Length],
                LevelToLoad = 1,
            };
            startInfo.CurrentLevel[0] = 1;
            
            for (int i = 1; i < packList.packs.Length; i++)
            {
                startInfo.CurrentLevel[i] = 1;
                startInfo.CompletedAmount[i] = -1;
            }

            return startInfo;
        }
        
        public void UpdateState() => 
            currentInfo = fileProvider.LoadData<PackStateInfo>(nameof(PackStateInfo), filePath);
        private void SaveState() => fileProvider.SaveData(currentInfo, nameof(PackStateInfo), filePath);
    }
}
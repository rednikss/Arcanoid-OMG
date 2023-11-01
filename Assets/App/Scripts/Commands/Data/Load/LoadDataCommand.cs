using System.IO;
using App.Scripts.Architecture.Patterns.Command;
using App.Scripts.Commands.Data.Save;
using UnityEngine;

namespace App.Scripts.Commands.Data.Load
{
    public class LoadDataCommand<T> : ICommand<T> where T : new()
    {
        private readonly string _fullPath;
        
        public T Data;

        public LoadDataCommand(string name, params string[] path)
        {
#if UNITY_EDITOR
            _fullPath = Path.Combine(Application.dataPath, Path.Combine(path), name);
#else
            _fullPath = Path.Combine(Application.persistentDataPath, name);
#endif
        }
        
        public T Execute()
        {
            if (!File.Exists(_fullPath))
            {
                var t = new T();
                new SaveDataCommand<T>(t, _fullPath).Execute();
                return t;
            }

            StreamReader streamReader = new(_fullPath);
            
            string json = streamReader.ReadToEnd();
            streamReader.Close();
            
            return JsonUtility.FromJson<T>(json) ;
        }
    }
}
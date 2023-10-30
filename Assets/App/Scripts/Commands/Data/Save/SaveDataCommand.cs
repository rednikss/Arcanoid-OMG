using System.IO;
using App.Scripts.Architecture.Command;
using UnityEngine;

namespace App.Scripts.Commands.Data.Save
{
    public class SaveDataCommand<T> : ICommand<bool> where T : new()
    {
        private readonly string _fullPath;
        
        private readonly T _data;

        public SaveDataCommand(T data, string name, params string[] path)
        {
#if UNITY_EDITOR
            _fullPath = Path.GetFullPath(Path.Combine(Application.dataPath, Path.Combine(path), name));
#else
            _fullPath = Path.GetFullPath(Path.Combine(Application.persistentDataPath, name));
#endif
            _data = data;
        }
        
        public SaveDataCommand(T data, string fullPath)
        {
            _fullPath = fullPath;
            _data = data;
        }
        
        public bool Execute()
        {
            FileStream fileStream = File.Open(_fullPath, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new(fileStream);
            
            string json = JsonUtility.ToJson(_data);
            streamWriter.Write(json);
            
            streamWriter.Close();
            fileStream.Close();
            
            return true;
        }
    }
}
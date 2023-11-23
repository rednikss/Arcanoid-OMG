using System.IO;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Data.DataParser;
using UnityEngine;

namespace App.Scripts.Libs.Utilities.Data.DataProvider
{
    public class FileDataProvider : MonoInstaller, IDataProvider
    {
        [SerializeField] private string format;
        
        private IDataParser _dataParser;
        
        public override void Init(ServiceContainer container)
        {
            _dataParser = container.GetService<IDataParser>();
        }
    
        private string GetFullFileName<TDataType>(string fileName)
        {
            return string.Format($"{fileName ?? typeof(TDataType).Name}.{format}");
        }

        public TDataType LoadData<TDataType>(string fileName, string filePath) where TDataType : new()
        {
#if UNITY_EDITOR
            string fullPath = Path.Combine(Application.dataPath, filePath, GetFullFileName<TDataType>(fileName));
#else
            string fullPath = Path.Combine(Application.persistentDataPath, GetFullFileName<TDataType>(fileName));
#endif
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("File not existing!");
                return new();
            }

            using StreamReader streamReader = new(fullPath);
            string unparsedData = streamReader.ReadToEnd();
            
            return _dataParser.Parse<TDataType>(unparsedData);
        }

        public void SaveData<TDataType>(TDataType data, string fileName, string filePath) where TDataType : new()
        {
#if UNITY_EDITOR
            string fullPath = Path.Combine(Application.dataPath, filePath, GetFullFileName<TDataType>(fileName));
#else
            string fullPath = Path.Combine(Application.persistentDataPath, GetFullFileName<TDataType>(fileName));
#endif
            
            string unparsedData = _dataParser.Convert(data);
            
            using StreamWriter streamWriter = new(File.Open(fullPath, FileMode.Create));
            streamWriter.Write(unparsedData);
        }
    }
}
using System.IO;
using App.Scripts.Architecture.Data.DataParser;
using App.Scripts.Architecture.InitPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Architecture.Data.DataLoader
{
    public class FileDataProvider : MonoInstaller, IDataProvider
    {
        [SerializeField] private string filesPath;

        [SerializeField] private string format;
        
        private IDataParser _dataParser;
        
        public override void Init(ProjectContext.ProjectContext context)
        {
            context.GetServiceContainer().SetServiceSelf<IDataProvider>(this);
            _dataParser = context.GetServiceContainer().GetService<IDataParser>();
        }
    
        public void LoadData<TDataType>(out TDataType data) where TDataType : new()
        {
#if UNITY_EDITOR
            string fullPath = Path.Combine(Application.dataPath, filesPath, GetFileName<TDataType>());
#else
            string fullPath = Path.Combine(Application.persistentDataPath, GetFileName<TDataType>());
#endif
            if (!File.Exists(fullPath))
            {
                data = new();
                SaveData(data);
                
                return;
            }
            
            StreamReader streamReader = new(fullPath);

            string unparsedData = streamReader.ReadToEnd();
            streamReader.Close();

            data = _dataParser.Parse<TDataType>(unparsedData);
        }

        public void SaveData<TDataType>(in TDataType data) where TDataType : new()
        {
#if UNITY_EDITOR
            string fullPath = Path.Combine(Application.dataPath, filesPath, GetFileName<TDataType>());
#else
            string fullPath = Path.Combine(Application.persistentDataPath, GetFileName<TDataType>());
#endif
            
            FileStream fileStream = File.Open(fullPath, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new(fileStream);
            
            string unparsedData = _dataParser.Convert(data);
            streamWriter.Write(unparsedData);
            
            streamWriter.Close();
            fileStream.Close();
        }

        private string GetFileName<TDataType>()
        {
            return string.Format($"{typeof(TDataType).Name}.{format}");
        }
        
    }
}
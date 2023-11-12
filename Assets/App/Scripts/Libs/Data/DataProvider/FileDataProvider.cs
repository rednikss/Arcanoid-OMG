using System.IO;
using App.Scripts.Libs.Data.DataParser;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Libs.Data.DataProvider
{
    public class FileDataProvider : MonoInstaller, IDataProvider
    {
        [SerializeField] private string filesPath;

        [SerializeField] private string format;
        
        private IDataParser _dataParser;
        
        public override void Init(ProjectContext.ProjectContext context)
        {
            context.GetContainer().SetServiceSelf<IDataProvider>(this);
            _dataParser = context.GetContainer().GetService<IDataParser>();
        }
    
        public void LoadData<TDataType>(out TDataType data, string fileName = null) where TDataType : new()
        {
#if UNITY_EDITOR
            string fullPath = Path.Combine(Application.dataPath, filesPath, GetFullFileName<TDataType>(fileName));
#else
            string fullPath = Path.Combine(Application.persistentDataPath, GetFullFileName<TDataType>(fileName));
#endif
            if (!File.Exists(fullPath))
            {
                data = new();
                SaveData(data, fileName);
                
                return;
            }
            
            StreamReader streamReader = new(fullPath);

            string unparsedData = streamReader.ReadToEnd();
            streamReader.Close();

            data = _dataParser.Parse<TDataType>(unparsedData);
        }

        public void SaveData<TDataType>(in TDataType data, string fileName = null) where TDataType : new()
        {
#if UNITY_EDITOR
            string fullPath = Path.Combine(Application.dataPath, filesPath, GetFullFileName<TDataType>(fileName));
#else
            string fullPath = Path.Combine(Application.persistentDataPath, GetFullFileName<TDataType>(fileName));
#endif
            
            FileStream fileStream = File.Open(fullPath, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new(fileStream);
            
            string unparsedData = _dataParser.Convert(data);
            streamWriter.Write(unparsedData);
            
            streamWriter.Close();
            fileStream.Close();
        }

        private string GetFullFileName<TDataType>(string fileName)
        {
            return string.Format($"{fileName ?? typeof(TDataType).Name}.{format}");
        }
        
    }
}
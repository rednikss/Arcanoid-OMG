using System;
using System.IO;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Utilities.Data.DataParser;
using UnityEngine;

namespace App.Scripts.Libs.Utilities.Data.DataProvider
{
    public class ResourcesTextDataProvider : MonoInstaller, IDataProvider
    {
        private IDataParser parser;
        
        public override void Init(ProjectContext.ProjectContext context)
        {
            parser = context.GetContainer().GetService<IDataParser>();
        }

        public TDataType LoadData<TDataType>(string fileName, string filePath) where TDataType : new()
        {
            var path = Path.Combine(filePath, fileName);
            
            string assetData = Resources.Load<TextAsset>(path).text;
            
            return parser.Parse<TDataType>(assetData);
        }

        public void SaveData<TDataType>(TDataType data, string fileName, string filePath) where TDataType : new()
        {
            throw new NotImplementedException("You can't save resources!");
        }
    }
}
using System;

namespace App.Scripts.Libs.Utilities.Data.DataProvider
{
    public interface IDataProvider
    {
        public TDataType LoadData<TDataType>(string fileName, string filePath = null) where TDataType : new();

        public void SaveData<TDataType>(TDataType data, string fileName, string filePath) where TDataType : new();
    }
}
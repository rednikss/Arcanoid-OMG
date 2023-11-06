using App.Scripts.Libs.ServiceContainer;

namespace App.Scripts.Architecture.Data.DataLoader
{
    public interface IDataProvider
    {
        public void LoadData<TDataType>(out TDataType data) where TDataType : new();

        public void SaveData<TDataType>(in TDataType data) where TDataType : new();
    }
}
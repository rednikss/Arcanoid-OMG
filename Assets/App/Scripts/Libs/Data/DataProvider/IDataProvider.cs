namespace App.Scripts.Libs.Data.DataProvider
{
    public interface IDataProvider
    {
        public void LoadData<TDataType>(out TDataType data, string fileName = null) where TDataType : new();

        public void SaveData<TDataType>(in TDataType data, string fileName = null) where TDataType : new();
    }
}
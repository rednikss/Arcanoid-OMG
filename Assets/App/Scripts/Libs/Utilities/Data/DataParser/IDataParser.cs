namespace App.Scripts.Libs.Utilities.Data.DataParser
{
    public interface IDataParser
    {
        public TDataType Parse<TDataType>(string data);
        public string Convert<TDataType>(TDataType data);
    }
}
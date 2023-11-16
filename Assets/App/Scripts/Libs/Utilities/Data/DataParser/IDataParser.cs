namespace App.Scripts.Libs.Utilities.Data.DataParser
{
    public interface IDataParser
    {
        public TDataType Parse<TDataType>(in string data);
        public string Convert<TDataType>(in TDataType data);
    }
}
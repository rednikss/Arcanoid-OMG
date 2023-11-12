namespace App.Scripts.Libs.Data.DataParser
{
    public interface IDataParser
    {
        public TDataType Parse<TDataType>(in string data);
        public string Convert<TDataType>(in TDataType data);
    }
}
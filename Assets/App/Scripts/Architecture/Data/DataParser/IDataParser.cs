namespace App.Scripts.Architecture.Data.DataParser
{
    public interface IDataParser
    {
        public TDataType Parse<TDataType>(in string data);
        public string Convert<TDataType>(in TDataType data);
    }
}
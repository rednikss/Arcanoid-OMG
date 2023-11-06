namespace App.Scripts.Libs.Factory
{
    public interface IFactory<T>
    {
        public T Create();
    }
}
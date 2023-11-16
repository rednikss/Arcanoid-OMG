namespace App.Scripts.Libs.Patterns.Factory
{
    public interface IFactory<T>
    {
        public T Create();
    }
}
namespace App.Scripts.Architecture.Patterns.Factory
{
    public interface IFactory<T>
    {
        public T Create();
    }
}
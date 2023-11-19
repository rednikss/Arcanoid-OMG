namespace App.Scripts.Libs.Patterns.ObjectPool
{
    public interface IObjectPool<TObjectType>
    {
        public TObjectType Create();

        public TObjectType Get();

        public void ReturnObject(TObjectType pooledObject);

        public void TakeObject(TObjectType pooledObject);

        public void DestroyObject(TObjectType pooledObject);

        public void Clear(bool clearUsing);
    }
}
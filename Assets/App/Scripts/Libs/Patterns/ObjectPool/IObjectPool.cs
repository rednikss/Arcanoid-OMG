namespace App.Scripts.Libs.Patterns.ObjectPool
{
    public interface IObjectPool<TObjectType>
    {
        public TObjectType Create(int id);

        public TObjectType Get(int id);

        public void ReturnObject(TObjectType pooledObject, int id);

        public void TakeObject(TObjectType pooledObject, int id);

        public void DestroyObject(TObjectType pooledObject);

        public void Clear(bool clearUsing);
    }
}
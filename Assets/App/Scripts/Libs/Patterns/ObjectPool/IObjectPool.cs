namespace App.Scripts.Libs.Patterns.ObjectPool
{
    public interface IObjectPool<TObjectType>
    {
        public TObjectType Create();

        public TObjectType Get();

        public void Release(TObjectType pooledObject);
        
        public void OnReturnObject(TObjectType pooledObject);
        
        public void OnTakeObject(TObjectType pooledObject);

        public void OnDestroyObject(TObjectType pooledObject);

        public void Clear(bool clearUsing);
    }
}
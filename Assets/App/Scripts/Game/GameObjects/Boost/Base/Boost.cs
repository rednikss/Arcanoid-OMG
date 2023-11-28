using App.Scripts.Game.GameObjects.Boost.Base.Pool;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Base
{
    public class Boost : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private int id;
        public int ID => id;
        
        private BoostPool boostPool;
        
        public override void Init(ServiceContainer container)
        {
            boostPool = container.GetService<BoostPool>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out Platform.Platform _)) return;
            
            Debug.Log("SSSUUUUUUUUUUUUUUUU");
            boostPool.ReturnObject(this, ID);
        }

        private void OnBecameInvisible()
        {
            boostPool.ReturnObject(this, ID);
        }
    }
}
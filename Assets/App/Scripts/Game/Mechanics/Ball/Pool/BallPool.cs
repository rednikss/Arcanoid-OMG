using App.Scripts.Libs.Patterns.ObjectPool;

namespace App.Scripts.Game.Mechanics.Ball.Pool
{
    public class BallPool : ObjectPool<Ball>
    {
        public override void UpdateWithDT(float dt)
        {
            foreach (var system in UsingObjects)
            {
                system.UpdateWithDT(dt);
            }
        }

        protected override void PauseSystem()
        {
            foreach (var system in UsingObjects) system.IsPaused = true;
        }
        
        protected override void ResumeSystem()
        {
            foreach (var system in UsingObjects) system.IsPaused = false;
        }
        
        public void ReturnAll()
        {
            while (UsingObjects.Count > 0)
            {
                var block = UsingObjects[0];
                ReturnObject(block);
            }
        }
    }
}
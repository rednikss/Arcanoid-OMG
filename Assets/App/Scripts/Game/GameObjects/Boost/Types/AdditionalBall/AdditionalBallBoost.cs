using App.Scripts.Game.GameObjects.Ball.Pool;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Types.AdditionalBall
{
    public class AdditionalBallBoost : Base.Boost
    {
        protected override void OnCollected()
        {
            var ball = Container.GetService<BallPool>().Get();
            
            ball.transform.position = transform.position;
            ball.Velocity = Vector2.up;
            
            base.OnCollected();
        }
    }
}
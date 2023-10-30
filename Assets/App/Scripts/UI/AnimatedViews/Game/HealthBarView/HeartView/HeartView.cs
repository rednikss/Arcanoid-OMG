using DG.Tweening;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Game.HealthBarView.HeartView
{
    public class HeartView : MonoBehaviour
    {
        public void Show(Vector2 endPosition, float showTime)
        {
            transform.DOMoveX(endPosition.x, showTime)
                .SetEase(Ease.OutSine)
                .SetLink(gameObject);
            transform.DOMoveY(endPosition.y, showTime)
                .SetEase(Ease.InBack)
                .SetLink(gameObject);
        }

        public void Hide(float showTime)
        {
            transform.DOScale(Vector3.zero, showTime)
                .SetEase(Ease.OutExpo)
                .SetLink(gameObject)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
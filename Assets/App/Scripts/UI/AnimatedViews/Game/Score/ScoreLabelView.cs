using App.Scripts.UI.AnimatedViews.Basic.Int;
using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace App.Scripts.UI.AnimatedViews.Game.Score
{
    public class ScoreLabelView : AnimatedIntView
    {
        [SerializeField] [Min(0)] private float appearTime;
        
        [SerializeField] private Vector3 moveDirection;
        
        [SerializeField] [Min(0)] private float moveTime;

        [SerializeField] [Min(0)] private float disappearTime;
        
        private Sequence _sequence;
        
        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            
            _sequence = DOTween.Sequence().SetLink(gameObject);
            InitSequence();
        }

        private void InitSequence()
        {
            _sequence.Append(transform.DOScale(Vector3.one, appearTime).SetEase(Ease.OutBack));
            _sequence.Insert(0, transform.DOMove(transform.position + moveDirection, moveTime));
            _sequence.Append(transform.DOScale(Vector3.zero, disappearTime).SetEase(Ease.InBack).OnComplete(() =>
            {
                Destroy(gameObject);
            }));
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Game.HealthBarView
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private HeartView.HeartView prefab;

        [SerializeField] private Vector3 startOffsetPosition;
        
        [SerializeField] private Vector3 heartDelta;
        
        private readonly Stack<HeartView.HeartView> _hearts = new();

        private float _timeForSpawn;
        
        public void SetHearts(int heartCount, float time)
        {
            _timeForSpawn = time / heartCount;
            for (int i = 0; i < heartCount; i++)
            {
                AddHeart(_timeForSpawn);
            }
        }

        public void RemoveHeart()
        {
            if (_hearts.Count == 0) return;
            
            var heart = _hearts.Pop();
            if (heart != null)
            {
                heart.Hide(_timeForSpawn);
            }
        }

        public void AddHeart(float showTime)
        {
            AddHeart(transform.position + startOffsetPosition, showTime);
        }

        public void AddHeart(Vector3 position, float showTime)
        {
            var heart = Instantiate(prefab, transform);
            heart.transform.position = position;
            _hearts.Push(heart);
            
            Vector3 endPosition = transform.position + heartDelta * (_hearts.Count - 1);
            heart.Show(endPosition, showTime);
        }
    }
}
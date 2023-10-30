using App.Scripts.UI.AnimatedViews.Basic.Button.Scriptable;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.UI.AnimatedViews.Basic.Button
{
    public class AnimatedButtonView : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private UnityEngine.UI.Button button;

        [SerializeField] private ButtonOptionsScriptable scriptable;

        private Color _unpressedColor;

        public void Start()
        {
            _unpressedColor = button.image.color;
        }

        private void Press()
        {
            button.image.DOColor(scriptable.pressedColor, scriptable.animationTime)
                .SetUpdate(true)
                .SetLink(gameObject);
            transform.DOScale(Vector3.one * scriptable.pressedScale, scriptable.animationTime)
                .SetUpdate(true)
                .SetLink(gameObject);
        }

        private void UnPress()
        {
            button.image.DOColor(_unpressedColor, scriptable.animationTime)
                .SetUpdate(true)
                .SetLink(gameObject, LinkBehaviour.CompleteOnDisable);
            transform.DOScale(Vector3.one, scriptable.animationTime)
                .SetUpdate(true)
                .SetEase(Ease.OutBounce)
                .SetLink(gameObject, LinkBehaviour.CompleteOnDisable);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (button.interactable && !DOTween.IsTweening(transform)) Press();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UnPress();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UnPress();
        }
    }
}
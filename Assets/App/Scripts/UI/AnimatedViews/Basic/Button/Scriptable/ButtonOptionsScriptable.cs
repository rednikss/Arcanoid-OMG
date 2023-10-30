using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Basic.Button.Scriptable
{
    [CreateAssetMenu(fileName = "Button Animation", menuName = "Scriptable Object/View/Button Config", order = 0)]
    public class ButtonOptionsScriptable : ScriptableObject
    {
        [Min(0)] 
        public float animationTime;

        public Color pressedColor;
        
        [Range(0, 1)] 
        public float pressedScale;
    }
}
using TMPro;
using UnityEngine;

namespace App.Scripts.Architecture.Scene.Packs.Scriptables.Pack
{
    [CreateAssetMenu(fileName = "Pack", menuName = "Scriptable Object/Packs/Pack", order = 0)]
    public class PackScriptable : ScriptableObject
    {
        public string packNameKey;
        
        [Min(1)] public int levelCount;
        
        [Space(20)] [Header("View Options")]
        public Sprite icon;

        public Color mainViewColor;
        
        public Color secondaryViewColor;

        [Space(20)] [Header("Text Options")]
        public Material fontMaterial;
        
        public TMP_ColorGradient mainTextGradient;
        
        public TMP_ColorGradient secondaryTextGradient;
        
    }
}
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Boost.Scriptable
{
    [CreateAssetMenu(fileName = "Boost Sprite List", menuName = "Scriptable Object/View/Boost Sprites", order = 0)]
    public class BoostSpriteList : ScriptableObject
    {
        public Sprite[] sprites;
    }
}
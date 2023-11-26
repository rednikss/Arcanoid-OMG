using UnityEngine;

namespace App.Scripts.Game.Mechanics.Energy.Scriptable
{
    [CreateAssetMenu(fileName = "Energy Config", menuName = "Scriptable Object/Mechanics/Energy Config", order = 0)]
    public class EnergyScriptable : ScriptableObject
    {
        public int maxAmount;
        public int minAmount;
        public int defaultAmount;
        public int increaseAmount;
    }
}
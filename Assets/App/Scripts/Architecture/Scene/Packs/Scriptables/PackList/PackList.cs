using App.Scripts.Architecture.Scene.Packs.Scriptables.Pack;
using UnityEngine;

namespace App.Scripts.Architecture.Scene.Packs.Scriptables.PackList
{
    [CreateAssetMenu(fileName = "Pack List", menuName = "Scriptable Object/Packs/Pack List Config", order = 0)]
    public class PackList : ScriptableObject
    {        
        public PackScriptable blockedPack;

        public PackScriptable[] packs;
    }
}
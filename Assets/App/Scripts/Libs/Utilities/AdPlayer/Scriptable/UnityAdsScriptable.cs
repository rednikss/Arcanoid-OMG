using UnityEngine;

namespace App.Scripts.Libs.Utilities.AdPlayer.Scriptable
{
    [CreateAssetMenu(fileName = "Ads Options", menuName = "Scriptable Object/Base/Ads Config", order = 0)]
    public class UnityAdsScriptable : ScriptableObject
    {
        public string gameID;
        public string adUnitID;
    }
}
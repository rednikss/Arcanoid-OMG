using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.AdPlayer.Scriptable;
using UnityEngine;
using UnityEngine.Advertisements;

namespace App.Scripts.Libs.Utilities.AdPlayer.InterstitialAdProvider
{
    public class UnityAdsProvider : MonoInstaller, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public bool isTesting;
        
        [SerializeField] private UnityAdsScriptable androidAdScriptable;
        
        [SerializeField] private UnityAdsScriptable iosAdScriptable;

        private UnityAdsScriptable current;
        public override void Init(ServiceContainer container)
        {
            if (Advertisement.isInitialized || !Advertisement.isSupported) return;

#if UNITY_ANDROID
            current = androidAdScriptable;
#elif UNITY_IOS
            current = iosAdScriptable;
#endif
            
            Advertisement.Initialize(current.gameID, isTesting, this);
            Advertisement.Load(current.adUnitID, this);
        }

        public void LoadAd()
        {
            Advertisement.Load(current.adUnitID, this);
        }
        
        public void ShowAd()
        {
            Advertisement.Show(current.adUnitID, this);
        }
        
        public void ShowAd(IUnityAdsShowListener listener)
        {
            Advertisement.Show(current.adUnitID, listener);
        }
        
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Error initializing Ads: {message}!");
        }
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit ({placementId}): {message}");
        }
        
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit ({placementId}): {message}");
        }
        
        public void OnInitializationComplete() { }

        public void OnUnityAdsAdLoaded(string placementId) { }

        public void OnUnityAdsShowStart(string placementId) { }

        public void OnUnityAdsShowClick(string placementId) { }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) { }

    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms
    public Character Character;
    void Awake()
    {   
        // 현재 플랫폼의 광고 단위 ID 가져오기:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // 광고가 표시(show)될 준비가 될 때까지 버튼 비활성화:
        _showAdButton.interactable = false;
    }
 
    // 광고를 표시(show)할 준비가 되려면 이 show 메서드를 호출하세요.
    public void LoadAd()
    {
        // 중요! 초기화 후 콘텐츠만 로드 (초기화는 다른 스크립트에서 처리됩니다.).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // 광고가 성공적으로 로드되면 버튼에 리스너를 추가하고 활성화합니다.:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
 
        if (adUnitId.Equals(_adUnitId))
        {
            // 클릭 시 ShowAd() 메서드를 호출하도록 버튼 구성:
            _showAdButton.onClick.AddListener(ShowAd);
            // 사용자가 클릭할 수 있는 버튼 활성화:
            _showAdButton.interactable = true;
        }
    }
 
    // 사용자가 버튼을 클릭할 때 실행할 메서드 구현:
    public void ShowAd()
    {
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }
 
    // Show Listener의 OnUnityAdsShowComplete 콜백 메서드를 구현하여 사용자가 보상을 받는지 확인합니다.:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            Character.movespeed *= 2f;
            LoadAd();
            // Grant a reward.
        }
    }
 
    // Load 및 Show Listener 오류 콜백 구현:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
 
    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}

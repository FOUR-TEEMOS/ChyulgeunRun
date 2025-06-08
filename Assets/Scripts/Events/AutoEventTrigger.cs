// ------------------------ AutoEventTrigger.cs ------------------------
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoEventTrigger : MonoBehaviour
{
    [Header("씬1용 이벤트 정보 (인스펙터에 드래그)")]
    public EventInfo autoEventInfo;

    private static bool _firstEventFired = false;

    // 플레이 모드 진입 시마다 이 메서드가 호출되어 플래그를 초기화합니다.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ResetFirstEventFlag()
    {
        _firstEventFired = false;
        Debug.Log("[AutoEventTrigger] _firstEventFired 리셋됨");
    }

    void Start()
    {
        // 이미 한 번 실행했으면 아무것도 안 함
        if (_firstEventFired) return;
        _firstEventFired = true;

        // 이벤트 인덱스 설정하고 게임 정지
        EventProgressManager.SetCurrentEventIndex(autoEventInfo.eventIndex);
        GameManager.Instance.PauseGame();

        // GameScene 위에 씬1을 Additive 로드
        SceneManager.LoadScene(autoEventInfo.eventSceneName, LoadSceneMode.Additive);
    }
}

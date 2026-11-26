// ------------------------ AutoEventTrigger.cs ------------------------
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoEventTrigger : MonoBehaviour
{
    [Header("씬1용 이벤트 정보 (인스펙터에 드래그)")]
    public EventInfo autoEventInfo;

    private bool _firstEventFired = false;

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

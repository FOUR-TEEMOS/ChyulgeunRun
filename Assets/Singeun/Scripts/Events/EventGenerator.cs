using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct EventInfo
{
    [Header("이벤트 이름")]
    public string eventName;

    [Header("이벤트에 해당하는 배경 인덱스 목록")]
    public List<int> backgroundIndices;

    [Header("이벤트 오브젝트")]
    public GameObject eventPrefab;

    [Header("이벤트 오브젝트를 생성할 위치")]
    public Vector3 eventObjectPos;

    [Header("이벤트 씬")]
    public string eventSceneName;
}

public class EventGenerator : MonoBehaviour
{
    [Header("이벤트 목록")]
    public List<EventInfo> eventList = new List<EventInfo>();

    [Header("이벤트 유지 배경 시간 (초)")]
    public float RETURN_TO_DEFAULT_AFTER = 30f;

    [Header("이벤트 재생성 시간 (초)")]
    public float respawnTime = 60f;

    private bool hasGenerated = false;
    private Collider2D triggerCollider;
    private BackgroundSpawner backgroundSpawner;
    private Moving mov;

    public bool test; // 임시 이벤트 선택지 테스트용

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();
        mov = GetComponent<Moving>();
        backgroundSpawner = GameObject.Find("BackgroundSpawner").GetComponent<BackgroundSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasGenerated) return;
        if (!collision.CompareTag("Player")) return;

        hasGenerated = true;
        GenerateEventSet();
        mov.speed = 0f;
    }

    private void GenerateEventSet()
    {
        // 랜덤으로 이벤트 선택
        int idx = Random.Range(0, eventList.Count);
        EventInfo selected = eventList[idx];
        Debug.Log($"[EventGenerator] 선택된 이벤트: {selected.eventName}");

        // 기존 배경 재생성 방지
        backgroundSpawner.StopCurrentBackgrounds();

        // 이벤트 배경 생성
        foreach (int bgIndex in selected.backgroundIndices)
        {
            backgroundSpawner.SpawnBg(bgIndex);
        }

        // 이벤트 오브젝트 생성 (EventGenerator의 위치 + eventOffset)
        Vector3 spawnPos = selected.eventObjectPos;
        GameObject evObj = Instantiate(selected.eventPrefab, spawnPos, Quaternion.identity);
        evObj.name = $"{selected.eventName}_EventObject";

        // 이벤트 씬을 Additive 모드로 로드
        SceneManager.LoadScene(selected.eventSceneName, LoadSceneMode.Additive);

        // 이벤트 (공사장 돌파한다 == true) 선택 -> 30초 후 기본 배경으로 돌아오기 + eventGenerator 재가동 코루틴 시작
        // 이벤트 (지나간다 == false) 선택 -> 바로 기본 배경 + eventGenerator 재가동 코루틴 시작
        if (test == true) RETURN_TO_DEFAULT_AFTER = 30f;
        else RETURN_TO_DEFAULT_AFTER = 0f;
        StartCoroutine(RestoreDefaultAfterSeconds(evObj, RETURN_TO_DEFAULT_AFTER));
    }

    private IEnumerator RestoreDefaultAfterSeconds(GameObject evObj, float RETURN_TO_DEFAULT_AFTER)
    {
        // 0초 or 30초 기다리기
        yield return new WaitForSeconds(RETURN_TO_DEFAULT_AFTER);

        // 이벤트용으로 생성했던 배경들 재생성 방지
        backgroundSpawner.StopCurrentBackgrounds();

        // 기본 배경 스트림을 다시 시작
        for (int i = 0; i < 4; i++)
        {
            backgroundSpawner.SpawnBg(i);
        }

        // 나머지 “respawnTime” 이후 EventGenerator 리셋 (다음 이벤트 대기 상태)
            yield return new WaitForSeconds(respawnTime);

        // 위치/속도/트리거 복원
        transform.position = new Vector3(8f, -2f, 0);
        mov.speed = 4f;
        hasGenerated = false;
        triggerCollider.enabled = true;
        Debug.Log("[EventGenerator] 기본 배경으로 복귀 → 다음 이벤트 대기 중");
    }
}

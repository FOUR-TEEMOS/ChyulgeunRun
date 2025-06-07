using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EventGenerator : MonoBehaviour
{
    [Header("이벤트 유지 배경 시간 (초)")]
    public float keepEventDuration = 0f;

    [Header("이벤트 재생성 시간 (초)")]
    public float respawnTime = 0f;

    // EventSelector가 골라서 넘겨 줄 EventInfo 데이터
    private EventInfo eventInfo;

    // 기본 배경 유지 여부
    public bool consistBackground;

    private bool hasGenerated = false;
    private BackgroundSpawner backgroundSpawner;
    private EventSeletor eventSeletor;

    public bool test; // 임시 이벤트 선택지 테스트용

    public void Initialize(EventInfo info)
    {
        eventInfo = info;
        consistBackground = eventInfo.consistBackground;
    }

    private void Awake()
    {
        backgroundSpawner = GameObject.Find("BackgroundSpawner").GetComponent<BackgroundSpawner>();
        eventSeletor = GameObject.Find("EventSeletor").GetComponent<EventSeletor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasGenerated) return;
        if (!collision.CompareTag("Player")) return;

        hasGenerated = true;
        GenerateEventSet(eventInfo);
    }

    private void GenerateEventSet(EventInfo eventInfo)
    {
        // 이벤트 씬을 Additive 모드로 로드
        SceneManager.LoadScene(eventInfo.eventSceneName, LoadSceneMode.Additive);

        //GameManager.Instance.PauseGame(); //임시

        // (공사장 지나간다 == false) 선택 -> 배경 유지
        if (test == false)
            consistBackground = true;

        // 배경이 바뀌어야 하는 이벤트 (공사장)
        if (!consistBackground)
        {
            // 기존 배경 재생성 방지
            backgroundSpawner.StopCurrentBackgrounds();

            // 이벤트 배경 생성
            foreach (int bgIndex in eventInfo.backgroundIndices)
            {
                backgroundSpawner.SpawnBg(bgIndex);
            }
        }

        if (!consistBackground)
        {
            // 배경이 바뀌는 이벤트(공사장)은 선택지에 따라 배경 바뀜 / 안 바뀜 이 정해짐
            // (공사장 돌파한다 == true) 선택 -> 45초 후 기본 배경으로 돌아오기 + eventSelector 재가동 코루틴 시작
            // (지나간다 == false) 선택 -> 바로 eventSelector 재가동 코루틴 시작
            if (test == true) keepEventDuration = 45f;
            else keepEventDuration = 0f;
        }
        StartCoroutine(RestoreDefaultAfterSeconds(keepEventDuration));
    }

    private IEnumerator RestoreDefaultAfterSeconds(float keepEventDuration)
    {
        // 0초 or 30초 기다리기
        yield return new WaitForSeconds(keepEventDuration);

        // 이벤트용으로 생성했던 배경들 재생성 방지
        if (!consistBackground)
        {
            backgroundSpawner.StopCurrentBackgrounds();

            // 기본 배경 스트림을 다시 시작
            for (int i = 0; i < 4; i++)
            {
                backgroundSpawner.SpawnBg(i);
            }
        }

        // 나머지 “respawnTime” 이후 다시 EventSelecotr 시작
        yield return new WaitForSeconds(respawnTime);

        // eventSeletor 복원 및 이벤트 오브젝트 제거
        eventSeletor.SetActiveTrue();
        eventSeletor.transform.position = new Vector3(8f, -2f, 0);
        Debug.Log("[EventObject] 기본 배경으로 복귀 → 다음 EventSelector 생성");
        Destroy(gameObject);
    }
}
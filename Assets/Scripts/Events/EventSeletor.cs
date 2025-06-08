using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventSeletor : MonoBehaviour
{
    [Header("이벤트 목록")]
    public List<EventInfo> eventList = new List<EventInfo>();

    private Collider2D triggerCollider;

    private bool hasSelected = false;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (GameManager.Instance.maxDistance - GameManager.Instance.currentDistance < 100f) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasSelected) return;
        if (!collision.CompareTag("Player")) return;

        float remain = GameManager.Instance.maxDistance - GameManager.Instance.currentDistance;
        hasSelected = true;
        triggerCollider.enabled = false;

        // 남은 거리가 700m 미만일 때는 0,2번을 제외한 후보 만들기 (공사장, 비옴 제외)
        List<EventInfo> candidates = new List<EventInfo>();
        for (int i = 0; i < eventList.Count; i++)
        {
            if (remain < 700f && (i == 0 || i == 2))
                continue;
            candidates.Add(eventList[i]);
        }

        // 랜덤으로 이벤트 선택
        int idx = Random.Range(0, candidates.Count);
        EventInfo selected = eventList[idx];
        Debug.Log($"[EventSelector] 선택된 이벤트: {selected.eventName}");

        // 이벤트 오브젝트 생성
        Vector3 spawnPos = selected.eventObjectPos;
        GameObject evObj = Instantiate(selected.eventPrefab, spawnPos, Quaternion.identity);
        evObj.name = $"{selected.eventName}_EventObject";

        // 생성된 오브젝트에서 EventGenerator 컴포넌트를 찾아서 선택됐던 EventInfo 데이터를 넘겨 줌
        EventGenerator handler = evObj.GetComponent<EventGenerator>();
        handler.Initialize(selected);

        EventProgressManager.SetCurrentEventIndex(selected.eventIndex); // 수정 event김명진

        // 이벤트 선택 후 비활성화
        gameObject.SetActive(false);
    }

    public void SetActiveTrue()
    {
        gameObject.SetActive(true);
        hasSelected = false;
        triggerCollider.enabled = true;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 개별 배경 요소 하나를 나타내는 구조체:
/// - prefab: 생성할 배경 GameObject
/// - spawnOffset: 기본 nextSpawnPoint 위치을 기준으로 이 요소가 생성될 오프셋
/// - moveSpeed: 이동 속도
/// </summary>
[System.Serializable]
public struct BackgroundComponent
{
    [Header("생성할 배경 요소 Prefab")]
    public GameObject prefab;

    [Header("nextSpawnPoint을 기준으로 이 요소가 생성될 오프셋")]
    public Vector3 spawnOffset;

    [Header("이동 속도")]
    public float moveSpeed;
}

/// <summary>
/// 이벤트 하나를 정의하는 구조체:
/// - eventName: Inspector에서 식별용
/// - backgroundComponents: 해당 이벤트에 속하는 여러 배경 요소 목록
/// - eventPrefab: 이벤트 오브젝트
/// - eventOffset: 배경이 생성된 뒤 이벤트 오브젝트가 나타날 위치 오프셋
/// - eventSceneName: 트리거 시 Additive 모드로 로드할 이벤트 씬 이름
/// </summary>
[System.Serializable]
public struct EventInfo
{
    [Header("이벤트 이름")]
    public string eventName;

    [Header("이벤트에 속하는 배경 요소들")]
    public List<BackgroundComponent> backgroundComponents;

    [Header("생성할 이벤트 오브젝트")]
    public GameObject eventPrefab;

    [Header("배경이 생성된 뒤 이벤트 오브젝트를 생성할 오프셋")]
    public Vector3 eventOffset;

    [Header("이벤트가 발생하면 로드할 씬 이름")]
    public string eventSceneName;
}

/// <summary>
/// EventGenerator:
/// - 플레이어가 트리거에 닿으면 랜덤 이벤트 선택 →
///   1) 해당 이벤트의 여러 배경 요소(BackgroundComponent)를 Instantiate하고, 각자 지정된 속도로 이동시키는 스크립트를 붙인다.
///   2) 이어서 이벤트 오브젝트(eventPrefab)를 Instantiate
///   3) LoadSceneMode.Additive로 eventSceneName 씬을 로드
///   4) respawnTime 후 hasGenerated를 false로 리셋하여 재활성화
/// </summary>
public class EventGenerator : MonoBehaviour
{
    [Header("1) 설정해 둔 이벤트 목록 (BackgroundComponents + EventObject + Scene 매핑)")]
    public List<EventInfo> eventList = new List<EventInfo>();

    [Header("2) 현재까지 이어진 배경 끝 위치 (X, Y, Z)")]
    public Vector3 nextSpawnPoint = Vector3.zero;

    [Header("3) 생성된 배경/이벤트 오브젝트 정리용 부모 객체")]
    public Transform backgroundParent;
    public Transform eventParent;

    [Header("4) 한 번 이벤트 생성 후 다시 생성 가능해질 때까지 대기할 시간(초)")]
    public float respawnTime = 10f;

    private bool hasGenerated = false;
    private Collider2D triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();

        // 만약 씬에 첫 배경이 이미 깔려 있다면,
        // nextSpawnPoint를 그 배경의 오른쪽 끝으로 미리 설정할 수 있습니다.
        // 예시:
        // GameObject firstBg = GameObject.Find("InitialBackground");
        // if (firstBg != null)
        // {
        //     SpriteRenderer sr = firstBg.GetComponent<SpriteRenderer>();
        //     nextSpawnPoint = new Vector3(sr.bounds.max.x, firstBg.transform.position.y, 0f);
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasGenerated) return;
        if (!collision.CompareTag("Player")) return;

        hasGenerated = true;
        GenerateEventSet();
    }

    private void GenerateEventSet()
    {
        // 1) 랜덤으로 이벤트 정보 선택
        int idx = Random.Range(0, eventList.Count);
        EventInfo selected = eventList[idx];
        Debug.Log($"[EventGenerator] Selected Event: {selected.eventName}");

        // 2) 선택된 이벤트에 속한 모든 배경 요소(BackgroundComponent)를 Instantiate
        //    각 요소는 nextSpawnPoint + spawnOffset 위치에 생성
        foreach (BackgroundComponent bgComp in selected.backgroundComponents)
        {
            Vector3 spawnPos = new Vector3(
                nextSpawnPoint.x + bgComp.spawnOffset.x,
                nextSpawnPoint.y + bgComp.spawnOffset.y,
                nextSpawnPoint.z + bgComp.spawnOffset.z
            );

            GameObject bgInstance = Instantiate(
                bgComp.prefab,
                spawnPos,
                Quaternion.identity,
                backgroundParent
            );
        }

        // 3) 모든 배경 요소를 생성한 이후, nextSpawnPoint.x를
        //    가장 큰 x-offset + 해당 요소 폭 값만큼 갱신해야 합니다.
        //    여기서는 가장 간단히, 배경 목록 중 마지막 요소의 폭으로 갱신한다고 가정.
        //    (실제 프로젝트에선 요소마다 폭이 다를 수 있으므로, 필요한 만큼 수정하세요.)
        {
            // 예시: 마지막으로 생성한 배경 요소의 SpriteRenderer.bounds.size.x를 사용
            BackgroundComponent lastComp = selected.backgroundComponents[selected.backgroundComponents.Count - 1];
            // 실제로 마지막으로 Instantiate한 객체를 추적하는 게 더 정확하지만, 
            // 간단히 Prefab의 SpriteRenderer 크기를 가져와 사용합니다.
            SpriteRenderer sr = lastComp.prefab.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                float maxWidth = sr.bounds.size.x;
                nextSpawnPoint = new Vector3(
                    nextSpawnPoint.x + maxWidth,
                    nextSpawnPoint.y,
                    nextSpawnPoint.z
                );
            }
            else
            {
                Debug.LogWarning($"[{selected.eventName}] 마지막 배경 요소 Prefab에 SpriteRenderer가 없습니다. nextSpawnPoint를 갱신하지 않습니다.");
            }
        }

        // 4) 이벤트 오브젝트 생성 (배경 전체를 기준으로 eventOffset만큼)
        Vector3 eventPos = new Vector3(
            nextSpawnPoint.x + selected.eventOffset.x,
            nextSpawnPoint.y + selected.eventOffset.y,
            nextSpawnPoint.z + selected.eventOffset.z
        );
        GameObject evInstance = Instantiate(
            selected.eventPrefab,
            eventPos,
            Quaternion.identity,
            eventParent
        );
        evInstance.name = $"{selected.eventName}_EventObject";

        // 5) 이벤트 씬을 Additive 모드로 로드
        if (!string.IsNullOrEmpty(selected.eventSceneName))
        {
            SceneManager.LoadScene(selected.eventSceneName, LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogWarning($"[{selected.eventName}] eventSceneName이 비어 있습니다. 씬을 로드하지 않습니다.");
        }

        // 6) respawnTime 후에 Generator를 재활성화
        StartCoroutine(ResetGenerator());
    }

    private IEnumerator ResetGenerator()
    {
        // 트리거 콜라이더를 잠시 비활성화하여
        // respawnTime 동안 다시 트리거되지 않게 막음
        triggerCollider.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        hasGenerated = false;
        triggerCollider.enabled = true;

        Debug.Log("[EventGenerator] 재활성화 완료 → 다음 이벤트를 대기 중");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightManager : MonoBehaviour
{
    [Header("벌레 오브젝트들")]
    public List<GameObject> bugs;

    [Header("전단지 오브젝트")]
    public List<GameObject> flyers;

    [Header("벌레를 보여줄 시간(초)")]
    public float bugDuration = 3f;
    [Header("전단지를 보여줄 시간(초)")]
    public float flyerDuration = 3f;

    void Start()
    {
        SetActiveList(bugs, false);
        SetActiveList(flyers, false);
    }

    // 공용 유틸: 리스트의 모든 오브젝트 On/Off
    private void SetActiveList(List<GameObject> list, bool on)
    {
        foreach (var go in list)
            go.SetActive(on);
    }

    // ----------------------------------------------------------------
    //  ? 벌레가 화면을 가리게 할 때 호출할 함수
    // ----------------------------------------------------------------
    public void CoverWithBugs()
    {
        StopCoroutine(nameof(ShowFlyersRoutine));
        StopCoroutine(nameof(ShowBugsRoutine));
        StartCoroutine(ShowBugsRoutine());
    }

    private IEnumerator ShowBugsRoutine()
    {
        SetActiveList(bugs, true);           // 벌레 켜고
        yield return new WaitForSeconds(bugDuration);
        SetActiveList(bugs, false);          // 벌레 끄기
    }

    // ----------------------------------------------------------------
    //  ? 전단지가 화면을 가리게 할 때 호출할 함수
    // ----------------------------------------------------------------
    public void CoverWithFlyers()
    {
        StopCoroutine(nameof(ShowBugsRoutine));
        StopCoroutine(nameof(ShowFlyersRoutine));
        StartCoroutine(ShowFlyersRoutine());
    }

    private IEnumerator ShowFlyersRoutine()
    {
        SetActiveList(flyers, true);         // 전단지 켜고
        yield return new WaitForSeconds(flyerDuration);
        SetActiveList(flyers, false);        // 전단지 끄기
    }
}

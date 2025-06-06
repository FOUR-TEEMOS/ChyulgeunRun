using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct EventInfo
{
    [Header("이벤트 이름")]
    public string eventName;

    [Header("이벤트에 해당하는 배경 인덱스 목록")]
    public List<int> backgroundIndices;

    [Header("이벤트 오브젝트(prefab)")]
    public GameObject eventPrefab;

    [Header("이벤트 오브젝트를 생성할 위치 (월드 좌표)")]
    public Vector3 eventObjectPos;

    [Header("이벤트용 씬 이름 (Additive 로드용)")]
    public string eventSceneName;

    [Header("배경이 유지되어야 하는 이벤트인가")]
    public bool consistBackground;
}

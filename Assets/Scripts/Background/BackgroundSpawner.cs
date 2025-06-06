using UnityEngine;
using System.Collections.Generic;

// This Script is for spawning Backgrounds
//
//    List<GameObject> Bg: gets Background prefabs from inspector
//                         Background prefabs must have backgroundManager with its unique number
// Vector 3 spawnPosition: gets spawnPosition from inspector
//                         The original Setting is Vector3(20.7f, -0.01181f, 0)
//
//         SpawnBg(int i): spawns Background Prefabs
//                         It requires a unique number of background prefab
//                         For showing various Environments, you should changed this script
//
//                         Bakcground Lists | buildings - 0
//                                          |     cloud - 1
//                                          |       sky - 2

[System.Serializable]
public struct BackgroundPrefabPosition
{
    [Header("생성할 배경 요소 Prefab")]
    public GameObject prefab;

    [Header("이 요소가 생성될 위치")]
    public Vector3 pos;
}


public class BackgroundSpawner : MonoBehaviour
{
    public List<BackgroundPrefabPosition> Bg;
    public Vector3 spawnPosition;
    private SpriteRenderer sr;

    public void SpawnBg(int i)
    {
        spawnPosition = Bg[i].pos;
        GameObject spawned = Instantiate(Bg[i].prefab, spawnPosition, Quaternion.identity);
        spawned.transform.SetParent(this.transform);

        // 현재 메인 배경은 Layer 올리기
        sr = spawned.GetComponentInChildren<SpriteRenderer>();
        int currentOrder = sr.sortingOrder;
        int newOrder = currentOrder;
        if (currentOrder == 0) newOrder = 3;    // 하늘 레이어
        else if (currentOrder == 1) newOrder = 4; // 구름/모래 레이어
        else if(currentOrder == 2) newOrder = 5; // 구조물 레이어

        sr.sortingOrder = newOrder;
    }

    public void StopCurrentBackgrounds()
    {
        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            BackgroundManager bgManager = this.transform.GetChild(i).gameObject.GetComponent<BackgroundManager>();
            bgManager.isOn = false;

            // 이제 안 쓰는 배경은 Layer 낮추기
            SpriteRenderer bgSR = this.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
            int currentOrder = bgSR.sortingOrder;
            int newOrder = currentOrder;
            if (currentOrder == 3) newOrder = 0; // 하늘
            else if (currentOrder == 4) newOrder = 1; // 구름 or 모래
            else if (currentOrder == 5) newOrder = 2; // 구조물들

            bgSR.sortingOrder = newOrder;
        }
    }
}

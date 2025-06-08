using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;
// This Script is for spawning Items
//
// List<WeightedObjects> prefabsToSpawn: gets Prefabs of Items and their Probability Weights from inspector
//                                       Prefabs of Items and their Probability Weights are given in struct 'WeightedObjects'                              
//          Vector2 spawnPositionOffset: gets Position for Spawning items from inspector
//                                       Generally, keep this instance (X: 0, Y: 0)
//                  float spawnInterval: gets Interval Time for Spawing items
//                                       It can be changed when time passes or background changes
//                                       The original setting is 2f     
//                          float timer: measures how long it's been
/*
 * ItemDataManager : getObsType() 추가
ItemSpawner : 가중치 계산이 한번에 여기서 되는데 일단 분리하기 힘들거같아 Timer 추가, Timer중에 패링 장애물 걸리면 다시돌림

 * _blockRemain 변수 추가 : PdCoffee 전용 타이머. || BlockSpawn() 추가, update 수정
 * 타이머 > 0 인 동안 장애물 생성 X. * 타이머는 수백개가 아닌 이상 부하가 조금도 가지 않는다고 함...!
 * 
 */
public class ItemSpawner : MonoBehaviour
{
    [Header("장애물 생성용")]
    public List<WeightedObjects> prefabsToSpawn;
    public Vector2 spawnPositionOffset;
    public float spawnInterval = 2f;
    public float paryObsCool = 20f; // 패링 장애물이 다시 나타날 시간
    public float paryObsTimer = 0f; // 패링 장애물 전용 쿨타이머
    public float _blockRemain = 0f; // PdCoffee 관련 타이머

    [Header("생성 예정 프리팹")]
    public GameObject selectedPrefab;
    private float timer = 0f;

    [Header("자판기기 생성용")]
    public GameObject vending_machine;
    public float cooltime_vending_machine = 60f;
    private float vendingTimer;
    public float vendingBlockWindow = 2f;
    private float lastVendingTime = -Mathf.Infinity;
    float sinceLastVending;
    bool blockAfter;
    bool blockBefore;
    bool vendingBlocking;

    [Header("커피 프리팹 (인스펙터에서 8번 슬롯으로 할당)")]
    public GameObject caffePrefab;


    [Header("끝 지점 연출용")]
    public GameObject companyBuildingPrefab;
    public float buildingSpawnDistance = 200f;   // 남은 거리 이 값 이하에서 스폰
    private bool buildingSpawned = false;

    void Start()
    {
        vendingTimer = cooltime_vending_machine;
        lastVendingTime = Time.time - cooltime_vending_machine;
    }

    void Update()
    {
        if (GameManager.Instance.isFrozen) return; // 붙잡힌 상태

        // =============== 마지막 연출 ====================
        // 남은 거리 계산
        float remain = GameManager.Instance.maxDistance - GameManager.Instance.currentDistance;

        // 마지막 연출 트리거
        if (!buildingSpawned && remain <= buildingSpawnDistance)
        {
            buildingSpawned = true;
            Vector3 spawnPos = new Vector3(24f, 1f, 0f);
            Instantiate(companyBuildingPrefab, spawnPos, Quaternion.identity, transform); // 회사 건물 생성
        }

        if (buildingSpawned) return;  // 건물 생성된 이후로는 스폰 로직은 건너뛰기

        // ================= 자판기 스폰 ====================
        vendingTimer -= Time.deltaTime;
        if (vendingTimer <= 0f)
        {
            Vector3 spawnPos = new Vector3(8f, -1.6f, 0);
            Instantiate(vending_machine, spawnPos, Quaternion.identity, transform);
            lastVendingTime = Time.time;
            vendingTimer = cooltime_vending_machine;
            GameManager.Instance.coffee_DrinkTimes += 1;
        }
        // 자판기 전후 2초 동안은 장애물 스폰 차단
        sinceLastVending = Time.time - lastVendingTime;
        blockAfter = sinceLastVending < vendingBlockWindow;      // 스폰 직후 2초
        blockBefore = vendingTimer < vendingBlockWindow;         // 스폰 직전 2초
        vendingBlocking = blockAfter || blockBefore;

        // ================= 장애물 스폰 =====================
        timer += Time.deltaTime;
        if (Time.timeScale == 0 || GameManager.Instance.caughtTimer > 0)
        {
            timer = 0f;
            return;
        }

        if (timer >= spawnInterval && _blockRemain <= 0f && !vendingBlocking)
        {
            SpawnPrefab();
            timer = 0f;
        }
        if (paryObsTimer > 0f)
            paryObsTimer -= Time.deltaTime;
        if (_blockRemain > 0f)
            _blockRemain -= Time.deltaTime;
    }

    void SpawnPrefab()
    {
        int num = prefabsToSpawn.Count;
        if (num == 0)
            return;

        if (selectedPrefab == null)
        { // 생성할 프리팹이 없으면 새로 설정.
            var candidates = new List<WeightedObjects>();
            float totalWeight = 0f;
            foreach (var obj in prefabsToSpawn)
            {
                // 'puddle' 은 비 오는 배경에서만 등장
                if (obj.prefab.name.Contains("puddle") && !GameManager.Instance.isRain)
                    continue;
                candidates.Add(obj);
                totalWeight += obj.weight;
            }
            if (totalWeight == 0f) return;

            float randomValue = Random.Range(0f, totalWeight);
            float accumulatedWeight = 0f;
            foreach (var obj in candidates)
            {
                accumulatedWeight += obj.weight;
                if (randomValue <= accumulatedWeight)
                {
                    selectedPrefab = obj.prefab;
                    if ((ItemDataManager.getObsType(selectedPrefab.name) != 0) && paryObsTimer > 0) // 패링 장애물 쿨타임이 남았는데 패링 장애물이 선택되면 다시돌림
                    {
                        Debug.Log("패링 장애물 쿨타임이 남아 다른 장애물로 재생성합니다.");
                        selectedPrefab = null;
                        SpawnPrefab();
                    }
                    break;
                }
            }
        }

        if (selectedPrefab == null)
            return;

        Vector3 spawnPos = new Vector3(
            transform.position.x + spawnPositionOffset.x,
            transform.position.y + spawnPositionOffset.y,
            0f
        );

        if (selectedPrefab != null)
        {
            GameObject spawned = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
            spawned.transform.SetParent(this.transform);
            if (ItemDataManager.getObsType(spawned.name) != 0)
            {
                paryObsTimer = paryObsCool;
            }
        }

        selectedPrefab = null; // 생성할 프리팹 초기화.
    }

    public void BlockSpawn(float time)
    {
        _blockRemain += time;
    }

    public void SpawnVendingMachine()
    {
        Vector3 spawnPos = new Vector3(8f, -1.6f, 0);
        Instantiate(vending_machine, spawnPos, Quaternion.identity, transform);
    }

    public void SpawnCaffe()
    {
        if (caffePrefab == null)
        {
            Debug.LogError("caffePrefab 이 할당되지 않았습니다!");
            return;
        }

        Vector3 spawnPos = new Vector3(24f, 1f, 0f);
        GameObject spawned = Instantiate(
            caffePrefab,
            spawnPos,
            Quaternion.identity,
            this.transform
        );
    }

}

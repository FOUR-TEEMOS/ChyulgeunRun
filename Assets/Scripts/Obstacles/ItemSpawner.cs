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
    public List<WeightedObjects> prefabsToSpawn;
    public Vector2 spawnPositionOffset;      
    public float spawnInterval = 3f;
    public float paryObsCool = 20f; // 패링 장애물이 다시 나타날 시간
    public float paryObsTimer = 0f; // 패링 장애물 전용 쿨타이머
    public float _blockRemain = 0f; // PdCoffee 관련 타이머

    [Header("생성 예정 프리팹")]
    public GameObject selectedPrefab;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (Time.timeScale == 0 || GameManager.Instance.caughtTimer > 0) return;

        if (timer >= spawnInterval && _blockRemain <= 0f)
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

            float totalWeight = 0f;
            foreach (var obj in prefabsToSpawn)
                totalWeight += obj.weight;

            float randomValue = Random.Range(0f, totalWeight);
            float accumulatedWeight = 0f;

            foreach (var obj in prefabsToSpawn)
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

}

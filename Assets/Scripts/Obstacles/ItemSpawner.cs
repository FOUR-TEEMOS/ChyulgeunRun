using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
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
public class ItemSpawner : MonoBehaviour
{
    public List<WeightedObjects> prefabsToSpawn;
    public Vector2 spawnPositionOffset;      
    public float spawnInterval = 3f;
    public float paryObsCool = 20f; // 패링 장애물이 다시 나타날 시간
    public float paryObsTimer = 0f;

    private float timer = 0f;
    private ItemDataManager ItemDataManager;

    private void Awake()
    {
        ItemDataManager = GameObject.Find("ItemDataManager").GetComponent<ItemDataManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f;
        }
        if (paryObsTimer > 0)
            paryObsTimer -= Time.deltaTime;
    }

    void SpawnPrefab()
    {
        int num = prefabsToSpawn.Count;
        if (num == 0)
            return;

        float totalWeight = 0f;
        foreach (var obj in prefabsToSpawn)
            totalWeight += obj.weight;

        float randomValue = Random.Range(0f, totalWeight);
        float accumulatedWeight = 0f;
        GameObject selectedPrefab = null;

        foreach (var obj in prefabsToSpawn)
        {
            accumulatedWeight += obj.weight;
            if (randomValue <= accumulatedWeight)
            {
                selectedPrefab = obj.prefab;
                Debug.Log($"{selectedPrefab.name}을 생성하겠습니다.");
                if ((ItemDataManager.getObsType(selectedPrefab.name) != 0) && paryObsTimer > 0) // 패링 장애물 쿨타임이 남았는데 패링 장애물이 선택되면 다시돌림
                {
                    Debug.Log("패링 장애물 쿨타임이 남아 재생성합니다.");
                    SpawnPrefab();
                    selectedPrefab = null;
                }
                break;
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
    }

}

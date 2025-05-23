using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Spawner : MonoBehaviour
{
    public List<WeightedObjects> prefabsToSpawn;
    public Transform obstacleTransform;
    public Vector2 spawnPositionOffset;       // 2D 위치 오프셋 (x, y)
    public float spawnInterval = 2f;           // 스폰 간격

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f;
        }
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
                break;
            }
        }

        if (selectedPrefab == null)
            return;

        // 오브젝트 생성
        Vector3 spawnPos = new Vector3(
            transform.position.x + spawnPositionOffset.x,
            transform.position.y + spawnPositionOffset.y,
            0f
        );

        GameObject spawned = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        spawned.transform.SetParent(this.transform);
    }
}

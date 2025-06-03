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

        Vector3 spawnPos = new Vector3(
            transform.position.x + spawnPositionOffset.x,
            transform.position.y + spawnPositionOffset.y,
            0f
        );
        
        if (selectedPrefab != null)
        {
            GameObject spawned = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
            spawned.transform.SetParent(this.transform);
        }
    }

}

using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
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
        if (prefabsToSpawn.Length == 0)
            return;

        int randomIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject prefab = prefabsToSpawn[randomIndex];

        Vector3 spawnPos = new Vector3(transform.position.x + spawnPositionOffset.x,
                                       transform.position.y + spawnPositionOffset.y,
                                       0f);

        GameObject spawnerer = Instantiate(prefab, spawnPos, Quaternion.identity);
        spawnerer.transform.SetParent(this.transform);
    }
}

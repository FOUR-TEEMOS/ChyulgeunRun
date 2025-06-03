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
public class BackgroundSpawner : MonoBehaviour
{
    public List<GameObject> Bg;
    public Vector3 spawnPosition;

    public void SpawnBg(int i)
    {
        GameObject spawned;
        if (i == 0)
        {
            spawnPosition = new Vector3(38f, 2.75f, 0);
        }
        else if (i == 5)
        {
            spawnPosition = new Vector3(18f, -2.1f, 0f);
        }
        else
        {
            spawnPosition = new Vector3(38f, 0f, 0f);
        }
            spawned = Instantiate(Bg[i], spawnPosition, Quaternion.identity);
            spawned.transform.SetParent(this.transform);
    }
}

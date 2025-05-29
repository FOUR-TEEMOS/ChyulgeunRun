using UnityEngine;

// This Script is for managing Backgrounds
//
// BackgroundSpawner backgroundSpawner: spawns Background Prefabs when the object collides with the object with tag 'disappear'
//                                      It requires a unique number of background prefab
//                                      This gets object when start time by Find function
//                               int i: gets a unique number of background prefab from inpspector
//
//                                      Bakcground Lists | buildings - 0
//                                                       |     cloud - 1
//
// COLLISION: When Backgrounds collide with the object with tag 'disappear', another prefab of this object would be spawned by Background Spawner
//            When Backgrounds collide with the object with tag 'disappearB', they would all disappear     
//            Eventually, Backgrounds would be repeating
//            For showing various Environments, you should changed this script

public class BackgroundManager : MonoBehaviour
{
    public BackgroundSpawner backgroundSpawner;
    public int i;
    public void Start()
    {
        backgroundSpawner = GameObject.Find("BackgroundSpawner").GetComponent<BackgroundSpawner>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("disappear"))
        {
            backgroundSpawner.SpawnBg(i);
        }
        else if (collision.gameObject.CompareTag("disappearB"))
        {
            Destroy(gameObject);
        }
    }
}

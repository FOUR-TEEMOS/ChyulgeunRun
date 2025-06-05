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
    public bool isOn = true;
//    public int thisBackgoundCount = 10;  // 구름 or 먼지같은 오브젝트가 제일 속도가 느려서 제일 마지막에 disappear에 닿음
    // 구름이 속도가 0.5고 빌딩 속도가 1이면, 구름 한 번 도착할 때 빌딩 2번 도착함
    // => 총 배경이 나오는 횟수가 정해지면 좋을듯

    public void Awake()
    {
        backgroundSpawner = GameObject.Find("BackgroundSpawner").GetComponent<BackgroundSpawner>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("disappear") && isOn) // 여기에 thisBackgoundCount > 0 조건 추가
        {
            backgroundSpawner.SpawnBg(i);
        }
        else if (collision.gameObject.CompareTag("disappearB"))
        {
            Destroy(gameObject);
        }
    }
}

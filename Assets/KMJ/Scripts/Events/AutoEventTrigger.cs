using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoEventTrigger : MonoBehaviour
{
    void Start()
    {
         //PlayerPrefs.DeleteAll();

        //Event1이 아직 안 했으면 EventScene1 로드
        if (!PlayerPrefs.HasKey("Event1_Done"))
        {
            EventProgressManager.SetCurrentEventIndex(1);
            SceneManager.LoadScene("EventScene1", LoadSceneMode.Additive); // GameScene 위에 추가로 로드
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoEventTrigger : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("Event1_Done"))
        {
            
            EventProgressManager.SetCurrentEventIndex(1);

            // 그 다음 씬 이동
            SceneManager.LoadScene("EventScene1");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoEventTrigger : MonoBehaviour
{
    void Start()
    {
    #if UNITY_EDITOR
        PlayerPrefs.DeleteAll(); // 에디터에서만 초기화
    #endif

        if (!PlayerPrefs.HasKey("Event1_Done"))
        {
            EventProgressManager.SetCurrentEventIndex(1);
            SceneManager.LoadScene("EventScene1");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoEventTrigger : MonoBehaviour
{
    void Start()
    {
        // 1️⃣ 이벤트 실행 여부 확인
        if (!PlayerPrefs.HasKey("Event1_Done"))
        {
            // 2️⃣ EventScene1으로 이동
            SceneManager.LoadScene("EventScene1");
        }
    }
}

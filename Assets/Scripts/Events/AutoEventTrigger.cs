using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoEventTrigger : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.DeleteKey("Event1_Done");//테스트용: 변수 강제초기화
        // 1️⃣ 이벤트 실행 여부 확인
        if (!PlayerPrefs.HasKey("Event1_Done"))
        {
            // 2️⃣ EventScene1으로 이동
            SceneManager.LoadScene("EventScene1");
        }
    }
}

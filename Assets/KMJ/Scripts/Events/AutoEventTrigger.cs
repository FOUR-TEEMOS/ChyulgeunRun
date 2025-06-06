using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoEventTrigger : MonoBehaviour
{
    void Start()
    {
        // 최초 1회만 PlayerPrefs 전체 초기화
        if (!PlayerPrefs.HasKey("Initialized"))
        {
            PlayerPrefs.DeleteAll();  // 전체 리셋
            PlayerPrefs.SetInt("Initialized", 1);  // 초기화 완료 마크 저장
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs 전체 초기화 완료!");
        }

        //Event1이 아직 안 했으면 EventScene1 로드
        if (!PlayerPrefs.HasKey("Event1_Done"))
        {
            EventProgressManager.SetCurrentEventIndex(1);
            SceneManager.LoadScene("EventScene1", LoadSceneMode.Additive); // GameScene 위에 추가로 로드
        }
    }
}

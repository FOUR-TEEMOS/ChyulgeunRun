using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 정신력 관련
    public float maxMental = 100;
    public float currentMental;

    // 게임 상태 관련
    public bool isGamePaused;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
    }

    void Start()
    {
        currentMental = maxMental;
    }

    // 정신력 감소
    public void TakeMentalDamage(float amount)
    {
        if (isGamePaused) return;

        currentMental -= amount;
        currentMental = Mathf.Clamp(currentMental, 0, maxMental);
        Debug.Log($"정신력 -{amount} -> {currentMental}");

        if (currentMental <= 0)
        {
            GameOver();
        }
    }

    // 정신력 회복
    public void RecoverMental(float amount)
    {
        if (isGamePaused) return;

        currentMental += amount;
        currentMental = Mathf.Clamp(currentMental, 0, maxMental);
        Debug.Log($"정신력 +{amount} -> {currentMental}");
    }

    // 일시정지
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        Debug.Log("게임 일시정지");
    }

    // 게임 재개
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        Debug.Log("게임 재개");
    }

    // 게임 오버
    private void GameOver()
    {
        PauseGame();
        Debug.Log("정신력 0 → 게임 오버 연출 트리거");
        // TODO: 엔딩 화면, 재시작 버튼 등
    }
}

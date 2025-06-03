using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("정신력")]
    public float maxMental = 100f;
    public float currentMental = 100f;
    public float mentalDrainRate = 1f; // 초당 정신력 감소량


    [Header("이동 거리")]
    public float currentDistance = 0f;
    public float maxDistance = 100f;  // 전체 목표 거리 (도착 기준선)
    public float moveSpeed = 1f;

    [Header("게임 속도")]
    public float baseSpeed = 1f;       // 기본 속도
    private float speedMultiplier = 1f;  // 아이템 등으로 배속 조정

    [Header("일시정지")]
    public bool isGamePaused;

    [Header("장애물 방어")]
    public bool protection = false;
    
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

    void Update()
    {
        if (!isGamePaused)
        {
            UpdateDistance();
            DrainMentalOverTime();
        }
    }

    // 정신력 감소
    public void TakeMentalDamage(float amount)
    {
        if (isGamePaused) return;
        
        currentMental -= amount;
        currentMental = Mathf.Clamp(currentMental, 0, maxMental);

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
    }

    // 정신력 서서히 감소
    private void DrainMentalOverTime()
    {
        TakeMentalDamage(mentalDrainRate * Time.deltaTime);
    }


    // 이동거리 관리리
    public void UpdateDistance()
    {
        // 시간 * 속도 = 거리
        currentDistance += Time.deltaTime * moveSpeed * baseSpeed * speedMultiplier;

        // (선택) 도착하면 거리 고정
        if (currentDistance >= maxDistance)
        {
            currentDistance = maxDistance;
            // GameClear() 등 호출 가능
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void ResetSpeedMultiplier()
    {
        speedMultiplier = 1f;
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

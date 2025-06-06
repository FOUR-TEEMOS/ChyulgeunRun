using UnityEngine;

/* usingSpeedUp 변수 추가 (시간으로 사용)
 * updateDistance 수정: usingSpeedUp 조건문 추가) 여기서 속도제어
 * setSpeedmultiplier 수정: 외부에서 usingSpeedUp 시간 조정가능
 * ResetSpeedmultiplier 삭제
 * -> 이렇게한 이유: 시간으로 안하면 중복으로 먹었을 때 중간에 끊김
 * ++ 속도 조정은 Moving 함수에서 진행됨
 * TakeMentalDamage 수정: 배속 중일때 피해 2배
 */
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("정신력")]
    public float maxMental = 100f;
    public float currentMental = 100f;
    public float mentalDrainRate = 1f; // 초당 정신력 감소량


    [Header("이동 거리")]
    public float currentDistance = 0f;
    public float maxDistance = 10000f;  // 전체 목표 거리 (도착 기준선)
    public float moveSpeed = 1f;

    [Header("게임 속도")]
    public float baseSpeed = 1f;       // 기본 속도
    private float speedMultiplier = 1f;  // 아이템 등으로 배속 조정
    private float usingSpeedUp = 0f;
    private float usingSpeedUpAmount = 1f;

    [Header("일시정지")]
    public bool isGamePaused;

    [Header("장애물 방어")]
    public bool protection = false;
    public bool superProtection = false; // 이건 이벤트:1번 관련변수

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
        if (usingSpeedUp > 0f) amount *= 2; // 배속 중이면 피해 2배

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


    // 이동거리 관리
    public void UpdateDistance()
    {

        if (usingSpeedUp > 0f)
        {
            speedMultiplier = usingSpeedUpAmount;
            usingSpeedUp -= Time.deltaTime;
        }
        else
        {
            superProtection = false;
            speedMultiplier = 1f;
        }

        // 시간 * 속도 = 거리
        currentDistance += Time.deltaTime * moveSpeed * baseSpeed * speedMultiplier;

        // (선택) 도착하면 거리 고정
        if (currentDistance >= maxDistance)
        {
            currentDistance = maxDistance;
            // GameClear() 등 호출 가능
        }
    }

    // 속도 배속시간 추가 (type 0: 일반, type 1: 무적)
    public void SetSpeedMultiplier(float time, float speed, int type)
    {
        Debug.Log($"{time}초 동안 {speed}배속 시작!");
        usingSpeedUpAmount = speed;
        usingSpeedUp = time;
        superProtection = false;
        if (type == 1)
            superProtection = true;
    }

    public float GetSpeedMultiplier()
    {
        return speedMultiplier;
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



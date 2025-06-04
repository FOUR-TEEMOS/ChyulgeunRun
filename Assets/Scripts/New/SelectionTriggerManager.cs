using UnityEngine;

public class SelectionTriggerManager : MonoBehaviour
{
    public SelectionTriggerManager Instance { get; private set; }
    private ItemSpawner itemSpawner;
    private float FastRoadGo_timer = 0f;
    private float RainyHurryUp_timer = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        itemSpawner = GameObject.Find("Spawner").GetComponent<ItemSpawner>();
    }

    // private으로 모두 구현하고 public, case로만 사용가능하게 제작
    public void SelectionTrigger(int num)
    {
        switch (num)
        {
            case 1: CoffeeDrink(); break;
            case 2: NotCoffeeDrink(); break;
            case 3: FastRoadGo(); break;
            case 4: NotFastRoadGo(); break;
            case 5: WalletOwnerFind(); break;
            case 6: WalletIgnore(); break;
            case 7: RainyHurryUp(); break;
            case 8: RainyUmbrella(); break;
            case 9: SchoolRunFight(); break;
            case 10: NotSchoolRunFight(); break;
            default: Debug.Log("없는 Selection Trigger"); break;
        }
    }


    private void CoffeeDrink()
    { // 50% 확률로 3초간 속도 3배 & 무적
        float chance = Random.value * 100f; // 0 ~ 100
        if (chance < 50f) return;
        GameManager.Instance.SetSpeedMultiplier(3f, 3f, 1);
    }

    private void NotCoffeeDrink()
    { // 위아래 반전

    }

    private void FastRoadGo()
    { // 15초 간 장애물 스폰시간 3초 -> 2초, 남은 거리 10% 감소 (* Update에서 처리)
        FastRoadGo_timer = 15f;
        GameManager.Instance.maxDistance -= GameManager.Instance.maxDistance / 10f;
    }

    private void NotFastRoadGo()
    { // 변화 없음

    }

    private void WalletOwnerFind()
    { // 커피 획득 (생성?) + 남은 거리 랜덤 증가

    }

    private void WalletIgnore()
    { // 변화 없음

    }

    private void RainyHurryUp()
    { // 10초 간 체력 소모 1.5배 증가 (* Update에서 처리)
        RainyHurryUp_timer = 10f;
    }

    private void RainyUmbrella()
    { // 물웅덩이 데미지 2배

    }

    private void SchoolRunFight()
    { // 남은 거리 10% 감소
        GameManager.Instance.maxDistance -= GameManager.Instance.maxDistance / 10f;
    }

    private void NotSchoolRunFight()
    { // 커피 자판기 생성

    }

    private void Update()
    {
        if (FastRoadGo_timer > 0f)
        {
            itemSpawner.spawnInterval = 2f;
            FastRoadGo_timer -= Time.deltaTime;
        }
        else itemSpawner.spawnInterval = 3f;

        if (RainyHurryUp_timer > 0f)
        {
            GameManager.Instance.mentalDrainRate = 1.5f;
            RainyHurryUp_timer -= Time.deltaTime;
        }
        else GameManager.Instance.mentalDrainRate = 1f;

    }

}

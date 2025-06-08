using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionTriggerManager : MonoBehaviour
{
    public static SelectionTriggerManager Instance { get; private set; }
    private ItemSpawner itemSpawner;
    private PlayerController playerController;
    private Camera camera;
    private List<WeightedObjects> coffeePool;
    private float FastRoadGo_timer = 0f;
    private float RainyHurryUp_timer = 0f;
    private float RainyUmbrella_timer = 0f;

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
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        camera = Camera.main; // 오류 생기면 :::::::::: 게임씬 아닌 씬은 maincamera 모두 끄기

        coffeePool = new List<WeightedObjects>(); // coffeePool에 커피만 추가하는 과정
        foreach (var w in itemSpawner.prefabsToSpawn)
            if (w.prefab.CompareTag("recovery"))
                coffeePool.Add(w);
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
            case 11: SamplingCoffee(); break;
            case 12: NonSamplingCoffee(); break;
            default: Debug.Log("없는 Selection Trigger"); break;
        }
    }


    private void CoffeeDrink()
    { // 50% 확률로 3초간 속도 3배 & 무적
        float chance = Random.value * 100f; // 0 ~ 100
        if (chance < 50f)
        {
            GameManager.Instance.SetSpeedMultiplier(3f, 3f, 1);
            Debug.Log("속도3배");
        }
        else
            StartCoroutine(CameraReverse(20f));;
    }

    private void NotCoffeeDrink()
    { // 위아래 반전
    }

    private void FastRoadGo()
    { // 45초 간 장애물 스폰시간 3초 -> 2초, 남은 거리 5% 감소 (* Update에서 처리)
        FastRoadGo_timer = 45f;
        GameManager.Instance.maxDistance -= GameManager.Instance.maxDistance / 20f;
    }

    private void NotFastRoadGo()
    { // 변화 없음
        // EventGenerator.Instance가 이미 배경을 바꿨다면
        var eg = EventGenerator.Instance;
        eg.consistBackground = true;

        // 즉시 공사장 배경 제거
        eg.backgroundSpawner.StopCurrentBackgrounds();
        // 기본 배경 다시 깔아 주기
        for (int i = 0; i < 5; i++)
            eg.backgroundSpawner.SpawnBg(i);
    }

    private void WalletOwnerFind()
    { // 자판기+ 남은 거리 5%증가
        if (coffeePool == null || coffeePool.Count == 0)
        {
            Debug.LogWarning("[SelectionTrigger] coffeePool is empty → WalletOwnerFind skipped");
            return;
        }

        // 능력 커피 생성 구현 필요
        itemSpawner.SpawnCaffe();

        GameManager.Instance.maxDistance += GameManager.Instance.maxDistance / 20f;
    }

    private void WalletIgnore()
    { // 변화 없음

    }

    private void RainyHurryUp()
    { // 10초 간 체력 소모 1.5배 증가 (* Update에서 처리), rainy 체크 X
        RainyHurryUp_timer = 10f;
    }

    private void RainyUmbrella()
    { // 30초 간 물웅덩이 데미지 2배, 선택지 자체가 비올때 등장하므로 rainy 체크 X
        RainyUmbrella_timer = 30f;
    }

    public float RainyUmbrellaTimer()
    {
        return RainyUmbrella_timer;
    }

    private void SchoolRunFight()
    { // 남은 거리 10% 감소
        GameManager.Instance.maxDistance -= GameManager.Instance.maxDistance / 10f;
    }

    private void NotSchoolRunFight()
    { // 커피 자판기 생성
        itemSpawner.SpawnVendingMachine();
    }

    private void SamplingCoffee()
    { // 정신력 회복, 50% 확률로 도를 아십니까 붙잡히기
        GameManager.Instance.RecoverMental(20f);

        float chance = Random.value * 100f; // 0 ~ 100
        if (chance < 50f) return;
        playerController.StartCoroutine(playerController.ResetCaught());
    }
    
    private void NonSamplingCoffee()
    {

    }

    private void Update()
    {
        if (FastRoadGo_timer > 0f)
        {
            itemSpawner.spawnInterval = 1.5f;
            FastRoadGo_timer -= Time.deltaTime;
        }
        else itemSpawner.spawnInterval = 3f;

        if (RainyHurryUp_timer > 0f)
        {
            GameManager.Instance.mentalDrainRate = 1.5f;
            RainyHurryUp_timer -= Time.deltaTime;
        }
        else GameManager.Instance.mentalDrainRate = 1f;

        if (RainyUmbrella_timer > 0f)
        {
            RainyUmbrella_timer -= Time.deltaTime;
        }

    }

    IEnumerator CameraReverse(float time)
    {
        camera.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        yield return new WaitForSeconds(time);
        camera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}

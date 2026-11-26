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
        camera = Camera.main; // ���� ����� :::::::::: ���Ӿ� �ƴ� ���� maincamera ��� ����

        coffeePool = new List<WeightedObjects>(); // coffeePool�� Ŀ�Ǹ� �߰��ϴ� ����
        foreach (var w in itemSpawner.prefabsToSpawn)
            if (w.prefab.CompareTag("recovery"))
                coffeePool.Add(w);
    }

    // private���� ��� �����ϰ� public, case�θ� ��밡���ϰ� ����
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
            default: Debug.Log("���� Selection Trigger"); break;
        }
    }


    private void CoffeeDrink()
    { // 50% Ȯ���� 3�ʰ� �ӵ� 3�� & ����
         if (Random.value < 0.04f)
            PlayerPrefs.SetInt("ifdrink", 1); // 오래된 커피를 마심 -> 4% 확률로 히든엔딩 5번
        float chance = Random.value * 100f; // 0 ~ 100
        if (chance < 50f)
        {
            GameManager.Instance.SetSpeedMultiplier(3f, 3f, 1);
            Debug.Log("�ӵ�3��");
        }
        else
            StartCoroutine(CameraReverse(20f));;
    }

    private void NotCoffeeDrink()
    { // ���Ʒ� ����
    }

    private void FastRoadGo()
    { // 45�� �� ��ֹ� �����ð� 3�� -> 2��, ���� �Ÿ� 5% ���� (* Update���� ó��)
        FastRoadGo_timer = 45f;
        GameManager.Instance.maxDistance -= GameManager.Instance.maxDistance / 20f;
    }

    private void NotFastRoadGo()
    { // ��ȭ ����
        // EventGenerator.Instance�� �̹� ����� �ٲ�ٸ�
        var eg = EventGenerator.Instance;
        eg.consistBackground = true;

        // ��� ������ ��� ����
        eg.backgroundSpawner.StopCurrentBackgrounds();
        // �⺻ ��� �ٽ� ��� �ֱ�
        for (int i = 0; i < 5; i++)
            eg.backgroundSpawner.SpawnBg(i);
    }

    private void WalletOwnerFind()
    { 
        // �ɷ� Ŀ�� ���� ���� �ʿ�
        itemSpawner.SpawnCaffe();

        GameManager.Instance.maxDistance += GameManager.Instance.maxDistance / 20f;
    }

    private void WalletIgnore()
    { // ��ȭ ����

    }

    private void RainyHurryUp()
    { // 10�� �� ü�� �Ҹ� 1.5�� ���� (* Update���� ó��), rainy üũ X
        RainyHurryUp_timer = 10f;
    }

    private void RainyUmbrella()
    { // 30�� �� �������� ������ 2��, ������ ��ü�� ��ö� �����ϹǷ� rainy üũ X
        RainyUmbrella_timer = 30f;
    }

    public float RainyUmbrellaTimer()
    {
        return RainyUmbrella_timer;
    }

    private void SchoolRunFight()
    { // ���� �Ÿ� 10% ����
        GameManager.Instance.maxDistance -= GameManager.Instance.maxDistance / 10f;
    }

    private void NotSchoolRunFight()
    { // Ŀ�� ���Ǳ� ����
        itemSpawner.SpawnVendingMachine();
    }

    private void SamplingCoffee()
    { // ���ŷ� ȸ��, 50% Ȯ���� ���� �ƽʴϱ� ��������
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

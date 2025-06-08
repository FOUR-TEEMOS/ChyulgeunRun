using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CoffeePopup : MonoBehaviour
{
    public static CoffeePopup Instance { get; private set; }

    [Header("CoffeePopup UI")]
    public Image coffeeImage;
    public Image coffeeBackground;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI detailText;
    public Button confirmButton;

    // 팝업에 전달할 커피 오브젝트
    private Coffees pendingCoffee;

    void Awake()
    {
        // 싱글턴 세팅
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        confirmButton.onClick.AddListener(OnConfirm);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 외부에서 팝업 띄울 때 호출
    /// </summary>
    public void Show(Coffees coffee)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;

        pendingCoffee = coffee;

        coffeeImage.sprite = pendingCoffee.icon;
        coffeeImage.preserveAspect = true;

        string detail = pendingCoffee.ExplainDetail();
        detailText.text = detail;

        titleText.text = $"{pendingCoffee.name}";
        gameObject.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    /// <summary>
    /// 확인 버튼 눌렀을 때
    /// </summary>
    private void OnConfirm()
    {
        GameManager.Instance.ResumeGame();
        // 능력 발동
        pendingCoffee.Init();
        pendingCoffee.eatCoffee();

        // 팝업 닫기
        gameObject.SetActive(false);
    }
}

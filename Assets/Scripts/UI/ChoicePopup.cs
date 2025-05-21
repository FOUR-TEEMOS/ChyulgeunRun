using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChoicePopup : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Button[] optionButtons;

    void Start()
    {
        ShowTestPopup();  // 테스트용
    }

    public void ShowTestPopup()
    {
        titleText.text = "👝 시민이 지갑을 떨어뜨렸습니다.";
        descriptionText.text = "주울까요? 무시할까요?";

        optionButtons[0].GetComponentInChildren<TMP_Text>().text = "주운다";
        optionButtons[1].GetComponentInChildren<TMP_Text>().text = "무시한다";

        optionButtons[0].onClick.RemoveAllListeners();
        optionButtons[1].onClick.RemoveAllListeners();

        optionButtons[0].onClick.AddListener(() => {
            //GameStateManager.Instance.ApplyEffect(+5, -2); // 정신력 +5, 시간 -2초
            SceneManager.LoadScene("GameScene");
        });

        optionButtons[1].onClick.AddListener(() => {
            //GameStateManager.Instance.ApplyEffect(0, 0);
            SceneManager.LoadScene("GameScene");
        });
    }
}

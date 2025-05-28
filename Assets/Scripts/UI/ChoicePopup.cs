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
        ShowEventChoices();
    }

   public void ShowEventChoices()
{
    int eventIndex = EventProgressManager.GetCurrentEventIndex();

    switch (eventIndex)
    {
        case 1:
            titleText.text = "☕ 가방 속 오래된 커피 발견!";
            descriptionText.text = "출근길 가방 속에서 오래된 캔커피를 발견했습니다.\n마실까요, 말까요?";
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = "마신다";
            optionButtons[1].GetComponentInChildren<TMP_Text>().text = "안 마신다";
            break;

        case 2:
            titleText.text = "🚧 공사 중인 지름길";
            descriptionText.text = "길이 막혀서 공사 중인 지름길을 발견했습니다.\n감행할까요, 돌아갈까요?";
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = "감행한다";
            optionButtons[1].GetComponentInChildren<TMP_Text>().text = "돌아간다";
            break;

        case 3:
            titleText.text = "👜 한 시민의 지갑을 줍다";
            descriptionText.text = "길에서 지갑을 줍습니다.\n주인에게 돌려줄까요?";
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = "돌려준다";
            optionButtons[1].GetComponentInChildren<TMP_Text>().text = "무시한다";
            break;

        case 4:
            titleText.text = "🌧 갑자기 비가 내리다";
            descriptionText.text = "비가 갑자기 내립니다.\n우산을 쓸까요, 뛰어갈까요?";
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = "뛰어간다";
            optionButtons[1].GetComponentInChildren<TMP_Text>().text = "우산 쓴다";
            break;

        case 5:
            titleText.text = "🏫 학교 단체 인파";
            descriptionText.text = "학교 앞에서 단체 인파가 지나가고 있습니다.\n뚫고 갈까요, 돌아갈까요?";
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = "뚫고 간다";
            optionButtons[1].GetComponentInChildren<TMP_Text>().text = "돌아간다";
            break;

        case 6:
            titleText.text = "☕ 커피 샘플링 이벤트";
            descriptionText.text = "직원이 나눠주는 커피를 받을까요?\n받으면 정신력 회복 but 랜덤으로 도를 아십니까 등장 가능";
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = "커피 받기";
            optionButtons[1].GetComponentInChildren<TMP_Text>().text = "무시";
            break;

        default:
            Debug.Log("잘못된 이벤트 번호");
            break;
    }

    optionButtons[0].onClick.RemoveAllListeners();
    optionButtons[1].onClick.RemoveAllListeners();

    optionButtons[0].onClick.AddListener(() => {
        ApplyEventResult(eventIndex, 0);
        EventProgressManager.AdvanceEventIndex();
        SceneManager.LoadScene("GameScene");
    });

    optionButtons[1].onClick.AddListener(() => {
        ApplyEventResult(eventIndex, 1);
        EventProgressManager.AdvanceEventIndex();
        SceneManager.LoadScene("GameScene");
    });
}
    void ApplyEventResult(int eventIndex, int choice)
{
    switch (eventIndex)
    {
        case 1: // 커피 마신다
            if (choice == 0)
            {
                GameManager.Instance.RecoverMental(20);
                // TODO: 50% 확률로 반전 상태 넣고 싶으면 여기서
            }
            break;

        case 2: // 감행
            if (choice == 0)
            {   
                GameManager.Instance.TakeMentalDamage(10);
                // TODO: 장애물 등장 로직 추가
            }
            break;

        case 3: // 지갑 줍는다
            if (choice == 0)
            {
                GameManager.Instance.RecoverMental(5);
                // TODO: 시간 랜덤 소모 추가 가능
            }
            break;

        case 4: // 비
            if (choice == 0)
            {
                GameManager.Instance.TakeMentalDamage(3);
            }
            break;

        case 6: // 커피 샘플링
            if (choice == 0)
            {
                GameManager.Instance.RecoverMental(5);
                // TODO: "도를 아십니까?" 추가 플래그 필요 시 여기에
            }
            break;
    }
}

}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChoicePopup : MonoBehaviour
{
    public TMP_Text titleText;           // 제목 텍스트
    public TMP_Text descriptionText;     // 설명 텍스트
    public Button[] optionButtons;       // 선택지 버튼 2개

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
                titleText.text = "가방 속 오래된 커피 발견!";
                descriptionText.text = "출근길 가방 속에서 오래된 캔커피를 발견했습니다.\n마실까요, 말까요?";
                SetOptions("마신다", "안 마신다");
                break;
            case 2:
                titleText.text = "공사 중인 지름길";
                descriptionText.text = "공사 중인 지름길을 발견했습니다.\n감행할까요, 돌아갈까요?";
                SetOptions("감행한다", "돌아간다");
                break;
            case 3:
                titleText.text = "한 시민의 지갑을 줍다";
                descriptionText.text = "길에서 지갑을 줍습니다.\n주인에게 돌려줄까요, 그냥 무시할까요?";
                SetOptions("돌려준다", "무시한다");
                break;
            case 4:
                titleText.text = "갑자기 비가 내리다";
                descriptionText.text = "비가 갑자기 내립니다.\n우산을 쓸까요, 그냥 뛸까요?";
                SetOptions("우산 쓴다", "뛰어간다");
                break;
            case 5:
                titleText.text = "학교 단체 인파";
                descriptionText.text = "단체 인파가 지나갑니다.\n뚫고 갈까요, 돌아갈까요?";
                SetOptions("뚫고 간다", "돌아간다");
                break;
            case 6:
                titleText.text = "커피 샘플링 이벤트!";
                descriptionText.text = "직원이 샘플 커피를 나눠줍니다. 받으시겠습니까?";
                SetOptions("받는다", "무시한다");
                break;
            default:
                Debug.LogWarning("잘못된 이벤트 인덱스입니다.");
                break;
        }

        // 버튼 리스너 초기화
        optionButtons[0].onClick.RemoveAllListeners();
        optionButtons[1].onClick.RemoveAllListeners();

        // 선택에 따라 SelectionTrigger 호출
        optionButtons[0].onClick.AddListener(() =>
        {
            //SelectionTriggerManager.Instance.SelectionTrigger(eventIndex * 2 - 1); // 1번 선택
            CompleteEvent(eventIndex);
        });

        optionButtons[1].onClick.AddListener(() =>
        {
            //SelectionTriggerManager.Instance.SelectionTrigger(eventIndex * 2);     // 2번 선택
            CompleteEvent(eventIndex);
        });
    }

    void SetOptions(string first, string second)
    {
        optionButtons[0].GetComponentInChildren<TMP_Text>().text = first;
        optionButtons[1].GetComponentInChildren<TMP_Text>().text = second;
    }

    void CompleteEvent(int eventIndex)
    {
        PlayerPrefs.SetInt($"Event{eventIndex}_Done", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}

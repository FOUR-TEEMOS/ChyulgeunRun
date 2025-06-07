using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
        Debug.Log("Current Event Index: " + eventIndex);  // 추가

        switch (eventIndex)
        {
            case 0:
                titleText.text = "⚠ 이벤트 인덱스 0 (디버그용)";
                descriptionText.text = "현재 EventProgressManager의 인덱스가 0입니다.";
                break;
            case 1: // 수정 event김명진
                titleText.text = "가방 속 오래된 커피 발견!";
                descriptionText.text = "출근길 가방 속에서 오래된 캔커피를 발견했습니다.\n마실까요, 말까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "마신다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "안 마신다";
                break;

            case 2: // 수정 event김명진
                titleText.text = "공사 중인 지름길";
                descriptionText.text = "길이 막혀서 공사 중인 지름길을 발견했습니다.\n들어갈까요, 돌아갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "들어간다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "돌아간다";
                break;

            case 3: // 수정 event김명진
                titleText.text = "한 시민의 지갑을 줍다";
                descriptionText.text = "길에서 지갑을 줍습니다.\n주인에게 돌려줄까요, 그냥 가지나요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "돌려준다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "무시한다";
                break;

            case 4: // 수정 event김명진
                titleText.text = "갑자기 비가 내리다";
                descriptionText.text = "비가 갑자기 내립니다.\n우산을 쓸까요, 뛰어갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "우산 쓴다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "뛰어간다";
                break;

            case 5: // 수정 event김명진
                titleText.text = "학교 단체 인파";
                descriptionText.text = "학교 앞에서 단체 인파가 지나가고 있습니다.\n길을 막을까요, 피해갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "길을 막는다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "피해간다";
                break;

            case 6: // 수정 event김명진
                titleText.text = "커피 샘플링 이벤트!";
                descriptionText.text = "직원이 나눠주는 샘플 커피를 받으시겠습니까?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "받는다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "거절한다";
                break;

            default:
                titleText.text = "잘못된 이벤트";
                descriptionText.text = "이벤트 정보를 찾을 수 없습니다.";
                break;
        }

        optionButtons[0].onClick.RemoveAllListeners();
        optionButtons[1].onClick.RemoveAllListeners();

        optionButtons[0].onClick.AddListener(() => HandleChoice(true));
        optionButtons[1].onClick.AddListener(() => HandleChoice(false));
    }

    void HandleChoice(bool isFirstOption)
    {
        int index = EventProgressManager.GetCurrentEventIndex();

        // 실제 효과는 케이스별로 처리 필요
        // if (isFirstOption)
        // {
        //     Debug.Log("첫 번째 선택지 선택됨");
        //     SelectionTriggerManager.Instance.SelectionTrigger(index * 2 - 1);  // 홀수: 첫 번째 선택
        // }
        // else
        // {
        //     Debug.Log("두 번째 선택지 선택됨");
        //     SelectionTriggerManager.Instance.SelectionTrigger(index * 2);  // 짝수: 두 번째 선택
        // }

        EventProgressManager.MarkEventAsDone(index);

        // ✅ 정확한 EventScene 이름으로 언로드
        string sceneName = "EventScene" + index;
        SceneManager.UnloadSceneAsync(sceneName); // 수정: 정확한 씬 언로드
        GameManager.Instance.ResumeGame(); //게임 재개
    }

}
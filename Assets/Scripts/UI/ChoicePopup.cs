// ------------------------ ChoicePopup.cs ------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChoicePopup : MonoBehaviour
{
    [Header("기존 UI")]
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Button[] optionButtons;
    public GameObject choicesPanel;   // ChoicesPanel 오브젝트

    [Header("결과 UI")]
    public GameObject resultPanel;    // ResultPanel 오브젝트
    public TMP_Text resultText;       // 효과 설명 텍스트
    public Button confirmButton;      // 확인 버튼

    void Start()
    {
        // 시작할 때는 선택지만 보이게
        choicesPanel.SetActive(true);
        resultPanel.SetActive(false);
        ShowEventChoices();
    }

    public void ShowEventChoices()
    {
        int eventIndex = EventProgressManager.GetCurrentEventIndex();
        Debug.Log("Current Event Index: " + eventIndex);  // 추가

        switch (eventIndex)
        {
            case 1: // 수정 event김명진
                titleText.text = "가방 속 오래된 커피 발견!";
                descriptionText.text = "출근을 하는 도중 가방 속에서 오래된 캔커피를 발견했습니다.\n마실까요, 말까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "마신다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "안 마신다";
                break;

            case 2: // 수정 event김명진
                titleText.text = "오늘부터 공사 중인가 보다..";
                descriptionText.text = "길이 막혀서 공사 중인 지름길을 발견했습니다.\n들어갈까요, 돌아갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "들어간다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "돌아간다";
                break;

            case 3: // 수정 event김명진
                titleText.text = "길을 가다 주인없는 지갑을 발견했습니다.";
                descriptionText.text = "일단 지갑을 줍습니다.\n주인에게 돌려줄까요, 그냥 가지나요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "돌려준다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "가진다";
                break;

            case 4: // 수정 event김명진
                titleText.text = "갑자기 비가 내린다..";
                descriptionText.text = "내 손에는 서류 가방이 들려있다.\n가방에는 중요한 서류들이 들어있다. 이걸쓴다면 분명 다 젖을거야 어쩌지?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "에라 모르겠다..!! 가방을 쓴다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "그냥 뛰어간다";
                break;

            case 5: // 수정 event김명진
                titleText.text = "등교 시간인가 봅니다..";
                descriptionText.text = "학교 앞에서 단체 인파가 지나가고 있습니다.\n 뚫고 지나갈까요, 피해갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "뚫고 지나간다";
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

        // 1) 실제 효과 적용
        if (isFirstOption)
            SelectionTriggerManager.Instance.SelectionTrigger(index * 2 - 1);
        else
            SelectionTriggerManager.Instance.SelectionTrigger(index * 2);

        EventProgressManager.MarkEventAsDone(index);

        // 2) 결과 패널 켜기
        string desc = GetEffectDescription(index, isFirstOption);
        resultText.text = desc;

        choicesPanel.SetActive(false);
        resultPanel.SetActive(true);

        // 3) 확인 버튼 리스너 등록 (한 번만)
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() =>
        {
            // 이벤트 씬 언로드 + 게임 재개
            string sceneName = "EventScene" + index;
            SceneManager.UnloadSceneAsync(sceneName);
            GameManager.Instance.ResumeGame();
        });
    }
        // 이벤트별 / 선택지별 효과 설명을 돌려주는 헬퍼
    string GetEffectDescription(int eventIndex, bool firstOption)
    {
        switch (eventIndex)
        {
            case 1:  // 커피 이벤트
                return firstOption
                    ? "50% 확률로 3초간 속도 3배 or 50%확률로 위아래 반전 적용"
                    : "아무일도 일어나지 않았습니다.";
            case 2:  // 지름길 이벤트
                return firstOption
                    ? "장애물 스폰 간격이 2초로 줄어듭니다, 남은 거리가 5% 감소됩니다."
                    : "아무일도 일어나지 않았습니다.";
            case 3://
                return firstOption
                    ? "자판기가 생성됩니다, 남은 거리가 5% 증가합니다."
                    : "아무일도 일어나지 않았습니다.";
            case 4://
                return firstOption
                    ? "10초간 체력 소모가 1.5배 증가합니다."
                    : "30초 간 물웅덩이 데미지가 2배가 됩니다.";            
            case 5://
                return firstOption
                    ? "남은 거리가 10% 감소합니다."
                    : "길을 돌아간 당신, 행운의 커피 자판기를 만납니다.";
            case 6://
                return firstOption
                    ? "정신력이 회복 됩니다 하지만 직원에게 붙잡힐 수도 있습니다."
                    : "아무일도 일어나지 않았습니다.";                    
            default:
                return "효과 정보를 찾을 수 없습니다.";
        }
    }

}
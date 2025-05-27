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

        // 이벤트별로 내용 다르게 설정
        switch (eventIndex)
        {
            case 2: // EventScene2: 가방 속 오래된 커피 발견
                titleText.text = "☕ 가방 속 오래된 커피 발견!";
                descriptionText.text = "출근길 가방 속에서 오래된 캔커피를 발견했습니다.\n마실까요, 말까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "마신다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "안 마신다";
                break;
                
            case 3: // EventScene3: 공사 중인 지름길
                titleText.text = "🚧 공사 중인 지름길";
                descriptionText.text = "길이 막혀서 공사 중인 지름길을 발견했습니다.\n들어갈까요, 돌아갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "들어간다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "돌아간다";
                break;
                
            case 4: // EventScene4: 한 시민의 지갑을 줍다
                titleText.text = "👜 한 시민의 지갑을 줍다";
                descriptionText.text = "길에서 지갑을 줍습니다.\n주인에게 돌려줄까요, 그냥 가지나요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "돌려준다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "무시한다";
                break;
                
            case 5: // EventScene5: 갑자기 비가 내리다
                titleText.text = "🌧 갑자기 비가 내리다";
                descriptionText.text = "비가 갑자기 내립니다.\n우산을 쓸까요, 뛰어갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "우산 쓴다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "뛰어간다";
                break;
                
            case 6: // EventScene6: 학교 단체 인파
                titleText.text = "🏫 학교 단체 인파";
                descriptionText.text = "학교 앞에서 단체 인파가 지나가고 있습니다.\n길을 막을까요, 피해갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "길을 막는다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "피해간다";
                break;
                
            default:
                Debug.Log("잘못된 이벤트 번호");
                break;
        }

        // 버튼 리스너 초기화
        optionButtons[0].onClick.RemoveAllListeners();
        optionButtons[1].onClick.RemoveAllListeners();

        // 선택 시 동작
        optionButtons[0].onClick.AddListener(() => {
            EventProgressManager.AdvanceEventIndex();
            SceneManager.LoadScene("GameScene");
        });

        optionButtons[1].onClick.AddListener(() => {
            EventProgressManager.AdvanceEventIndex();
            SceneManager.LoadScene("GameScene");
        });
    }
}

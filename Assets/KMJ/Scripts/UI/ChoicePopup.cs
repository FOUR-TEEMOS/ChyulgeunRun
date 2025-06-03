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

        // 제목/설명/버튼 텍스트 설정
        switch (eventIndex)
        {
            case 1:
                titleText.text = "가방 속 오래된 커피 발견!";
                descriptionText.text = "출근길 가방 속에서 오래된 캔커피를 발견했습니다.\n마실까요, 말까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "마신다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "안 마신다";
                break;

            case 2:
                titleText.text = "공사 중인 지름길";
                descriptionText.text = "길이 막혀서 공사 중인 지름길을 발견했습니다.\n들어갈까요, 돌아갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "들어간다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "돌아간다";
                break;

            case 3:
                titleText.text = "한 시민의 지갑을 줍다";
                descriptionText.text = "길에서 지갑을 줍습니다.\n주인에게 돌려줄까요, 그냥 가지나요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "돌려준다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "무시한다";
                break;

            case 4:
                titleText.text = "갑자기 비가 내리다";
                descriptionText.text = "비가 갑자기 내립니다.\n우산을 쓸까요, 뛰어갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "우산 쓴다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "뛰어간다";
                break;

            case 5:
                titleText.text = "학교 단체 인파";
                descriptionText.text = "학교 앞에서 단체 인파가 지나가고 있습니다.\n길을 막을까요, 피해갈까요?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "길을 막는다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "피해간다";
                break;

            case 6:
                titleText.text = "커피 샘플링 이벤트!";
                descriptionText.text = "직원이 나눠주는 샘플 커피를 받으시겠습니까?";
                optionButtons[0].GetComponentInChildren<TMP_Text>().text = "받는다";
                optionButtons[1].GetComponentInChildren<TMP_Text>().text = "거절한다";
                break;

            default:
                Debug.LogWarning("잘못된 이벤트 인덱스입니다.");
                break;
        }

        // 버튼 리스너 제거
        optionButtons[0].onClick.RemoveAllListeners();
        optionButtons[1].onClick.RemoveAllListeners();

        // 버튼 리스너 등록
        optionButtons[0].onClick.AddListener(() =>
        {
            HandleEventChoice(true, eventIndex);
        });

        optionButtons[1].onClick.AddListener(() =>
        {
            HandleEventChoice(false, eventIndex);
        });
    }

    void HandleEventChoice(bool isFirstOption, int eventIndex)
    {
        switch (eventIndex)
        {
            case 1:
                if (isFirstOption)
                {
                    if (Random.value < 0.5f)
                        GameManager.Instance.SetSpeedMultiplier(2f); // 속도 2배
                    else
                        Debug.Log("혼란 상태 (추후 구현)");
                }
                break;

            case 2:
                if (isFirstOption)
                {
                    // 공사길 감행: 장애물 많아짐은 ObstacleManager에서 구현 필요
                    GameManager.Instance.currentDistance += 10f; // 거리 보정
                }
                break;

            case 3:
                if (isFirstOption)
                {
                    // 지갑 돌려줌 → 커피 획득은 아이템 부여 시스템 필요
                    GameManager.Instance.currentDistance += Random.Range(5f, 10f); // 남은 거리 증가
                }
                break;

            case 4:
                if (!isFirstOption)
                {
                    GameManager.Instance.TakeMentalDamage(20f); // 체력 소모는 따로 처리 필요
                }
                break;

            case 5:
                if (isFirstOption)
                {
                    GameManager.Instance.currentDistance += 5f; // 거리 단축
                }
                else
                {
                    Debug.Log("카페 또는 자판기 등장 (추후 구현)");
                }
                break;

            case 6:
                if (isFirstOption)
                {
                    GameManager.Instance.RecoverMental(15f); // 정신력 회복은 필요 시 수정
                }
                break;
        }

        // 이벤트 완료 처리
        PlayerPrefs.SetInt($"Event{eventIndex}_Done", 1);
        PlayerPrefs.Save();

        // GameScene으로 복귀
        SceneManager.LoadScene("GameScene");
    }
}

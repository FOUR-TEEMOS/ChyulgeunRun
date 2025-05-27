using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChoicePopup : MonoBehaviour
{
    public TMP_Text titleText;           // 위쪽 제목 글자
    public TMP_Text descriptionText;     // 아래쪽 설명 글자
    public Button[] optionButtons;       // 버튼 2개

    void Start()
    {
        ShowEvent1();  // EventScene1이 실행되면 자동으로 이 함수 호출
    }

    public void ShowEvent1()
    {
        // 1️⃣ 제목 텍스트 설정
        titleText.text = "☕ 가방 속 오래된 커피 발견!";

        // 2️⃣ 설명 텍스트 설정
        descriptionText.text = "출근길 가방 속에서 오래된 캔커피를 발견했습니다.\n마실까요, 말까요?";

        // 3️⃣ 버튼 글자 설정
        optionButtons[0].GetComponentInChildren<TMP_Text>().text = "마신다";
        optionButtons[1].GetComponentInChildren<TMP_Text>().text = "안 마신다";

        // 4️⃣ 이전 리스너 제거
        optionButtons[0].onClick.RemoveAllListeners();
        optionButtons[1].onClick.RemoveAllListeners();

        // 5️⃣ 선택 시 동작 설정
        optionButtons[0].onClick.AddListener(() => {
            // 마신다 선택: 정신력 +5 (나중에 GameStateManager로 처리 가능)
            PlayerPrefs.SetInt("Event1_Done", 1);  // 이벤트 완료 저장
            PlayerPrefs.Save();                   // 디스크에 저장
            Debug.Log("마신다 선택됨!");
            SceneManager.LoadScene("GameScene");
        });

        optionButtons[1].onClick.AddListener(() => {
            // 안 마신다 선택: 아무 일도 없고 그냥 돌아감
            PlayerPrefs.SetInt("Event1_Done", 1);  // 이벤트 완료 저장
            PlayerPrefs.Save();                   // 디스크에 저장
            Debug.Log("안 마신다 선택됨!");
            SceneManager.LoadScene("GameScene");
        });
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTriggerManager : MonoBehaviour
{
    // NOT instance. ONLY exists in GameScene
    private bool _isDone = false;
    private void Update()
    {
        if (_isDone == true)
            return;

        bool clear = GameManager.Instance.currentDistance >= GameManager.Instance.maxDistance;
        bool deadMental = GameManager.Instance.currentMental <= 0f;

        if (!clear && !deadMental) return;

        _isDone = true;

        float chance = Random.value;

        int ending;
        if (deadMental) ending = 6;
        else if (GameManager.Instance.currentMental >= 80f) ending = 0;
        else if (PlayerPrefs.GetInt("ifdrink", -1) == 1) { PlayerPrefs.SetInt("ifdrink", 0); ending = 5; }
        else if (GameManager.Instance.do_HelloTimes > 5) ending = 7;
        else if (GameManager.Instance.coffee_DrinkTimes >= 5) ending = 8;
        else if (chance < (2 / 7)) ending = 9;
        else if (GameManager.Instance.currentMental >= 60f) ending = 1;
        else if (GameManager.Instance.currentMental >= 40f) ending = 2;
        else if (GameManager.Instance.currentMental >= 20f) ending = 3;
        else if (GameManager.Instance.currentMental >= 1f) ending = 4;
        else ending = 6;

        PlayerPrefs.SetInt("thisEnding", ending);
        ChangeScene.Instance.ChangeTo("EndingScene");

    }
}

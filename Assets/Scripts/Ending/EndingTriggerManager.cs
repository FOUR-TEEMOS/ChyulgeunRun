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

        int ending;
        if (deadMental) ending = 5;
        else if (GameManager.Instance.currentMental >= 100f) ending = 0;
        else if (GameManager.Instance.currentMental >= 75f) ending = 1;
        else if (GameManager.Instance.currentMental >= 50f) ending = 2;
        else if (GameManager.Instance.currentMental >= 25f) ending = 3;
        else if (GameManager.Instance.currentMental >= 1f) ending = 4;
        else ending = 5;

        PlayerPrefs.SetInt("thisEnding", ending);
        ChangeScene.Instance.ChangeTo("EndingScene");

    }
}

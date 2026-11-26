using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// This Script is for changing Scenes
//
// ChangeScene(): changes Scenes when the button clicked
//                You should change name of Scene after they're completely decided
//                To use this script properly, Add scenes to Scene List at [File-Build Profiles-Scene List]
//  
//                Button-Scene Lists |          StartButton - Obstacle
//                                   |    EndingListsButton - EndingListsScene
//                                   | CharacterListsButton - CharacterListsScene
//                                   |           BackButton - StartScene
public class SceneChanger : MonoBehaviour
{
    private static bool introSeen = false;
    public void ChangeScenes()
    {
        switch (gameObject.name)
        {
            case "StartButton":
                if (introSeen)
                {
                    // 두 번째 판부터: 인트로 없이 바로 게임
                    ChangeScene.Instance.ChangeTo("GameScene", 1f);
                }
                else
                {
                    // 첫 판: 인트로
                    introSeen = true;
                    ChangeScene.Instance.ChangeTo("StartScene 2", 1f);
                }
                break;
            case "StartGameButton":
                ChangeScene.Instance.ChangeTo("GameScene");
                break;
            case "EndingListsButton":
                ChangeScene.Instance.ChangeTo("EndingListsScene");
                break;
            case "CharacterListsButton":
                SceneManager.LoadScene("CharacterListsScene");
                break;
            case "BackButton":
                ChangeScene.Instance.ChangeTo("StartScene 1");
                break;
        }
    }
}

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
    public void ChangeScenes()
    {
        switch (gameObject.name)
        {
            case "StartButton":
                ChangeScene.Instance.ChangeTo("StartScene 2");
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

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
                //SceneManager.LoadScene("MainScene");
                ChangeScene.Instance.ChangeTo("StartScene 2");
                break;
            case "StartGameButton":
                //SceneManager.LoadScene("EndingListsScene");
                ChangeScene.Instance.ChangeTo("MainScene");
                break;
            case "CharacterListsButton":
                SceneManager.LoadScene("CharacterListsScene");
                break;
            case "BackButton":
                SceneManager.LoadScene("StartScene");
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ChangeScene : MonoBehaviour
{
    public CanvasGroup black;
    private float fadeDuration = 1f;


    public static ChangeScene Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    public void ChangeTo(string sceneName)
    {
        black.DOFade(1f, fadeDuration)
            .OnStart(() =>
            {
                black.blocksRaycasts = true;
            })
            .OnComplete(() =>
            { 
                SceneManager.LoadScene(sceneName);
            }); 
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        black.DOFade(0f, fadeDuration)
            .OnComplete(()=> { black.blocksRaycasts = false; });
    }







}

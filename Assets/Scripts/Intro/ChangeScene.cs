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
                StartCoroutine(LoadScene(sceneName));
            }); 
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        yield return new WaitUntil(() => async.progress >= 0.9f);

        async.allowSceneActivation = true;          // 실제 전환
        yield return async;                         // 완료까지 한 프레임 더 대기
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        black.DOFade(0f, fadeDuration * 2.5f)
            .OnComplete(()=> { black.blocksRaycasts = false; });
    }








}

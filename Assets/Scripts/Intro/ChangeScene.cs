using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ChangeScene : MonoBehaviour
{
    public CanvasGroup black;
    private float fadeDuration = 0.1f;


    public static ChangeScene Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
            return;
        }

        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    public void ChangeTo(string sceneName, float fadeOutDuration = -1f)
    {
        float duration = fadeOutDuration > 0f ? fadeOutDuration : fadeDuration;
        black.DOFade(0.5f, duration)
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

        async.allowSceneActivation = true;          // ���� ��ȯ
        yield return async;                         // �Ϸ���� �� ������ �� ���
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        black.DOFade(0f, fadeDuration)
            .OnComplete(()=> { black.blocksRaycasts = false; });
    }








}

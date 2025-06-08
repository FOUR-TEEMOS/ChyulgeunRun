using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartSceneTrigger : MonoBehaviour
{
    public Image one;
    public Image two;
    public Image two_2;
    public Image three;
    public Image four;
    public Button button;

    private void Awake()
    {
        //one.gameObject.SetActive(false);
        //two.gameObject.SetActive(false);
        //three.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine("Routine");
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(1f);
        Tween tween = one.DOFade(1f, 1.5f);
        yield return tween.WaitForCompletion();
        //yield return new WaitForSeconds(1f);
        tween = two.DOFade(1f, 1.5f);
        yield return tween.WaitForCompletion();
        //yield return new WaitForSeconds(1f);
        tween = two_2.rectTransform.DOMoveX(1386f, 0.2f);
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(1.3f);
        tween = three.DOFade(1f, 1.5f);
        yield return tween.WaitForCompletion();
        //yield return new WaitForSeconds(1f);
        tween = four.DOFade(1f, 0.1f);
        yield return tween.WaitForCompletion();
        tween = four.rectTransform.DOMoveX(1500f, 0.5f);
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(3f);
        //button.gameObject.SetActive(true);
        //ChangeScene.Instance.ChangeTo("GameScene");
        SceneManager.LoadScene("GameScene");

        yield return null;
    }


}

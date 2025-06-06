using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartSceneTrigger : MonoBehaviour
{
    public Image one;
    public Image two;
    public Image three;
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
        tween = two.DOFade(1f, 1.5f);
        yield return tween.WaitForCompletion();
        tween = three.DOFade(1f, 1.5f);
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        button.gameObject.SetActive(true);

        yield return null;
    }


}

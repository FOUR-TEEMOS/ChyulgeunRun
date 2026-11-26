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
        // 컷 1
        yield return PlayCut(DOTween.Sequence()
        .AppendInterval(1f)
        .Append(one.DOFade(1f, 1.5f)));

        // 컷 2
        yield return PlayCut(DOTween.Sequence()
        .Append(two.DOFade(1f, 1.5f))
        .Append(two_2.rectTransform.DOMoveX(1386f, 0.2f))
        .AppendInterval(1.3f));

        // 컷 3
        yield return PlayCut(DOTween.Sequence()
        .Append(three.DOFade(1f, 1.5f)));

        // 컷 4 (+ 끝나고 3초 대기)
        yield return PlayCut(DOTween.Sequence()
        .Append(four.DOFade(1f, 0.1f))
        .Append(four.rectTransform.DOMoveX(1500f, 0.5f))
        .AppendInterval(3f));

        SceneManager.LoadScene("GameScene");
    }
    // 컷 하나를 재생하다가 Space를 누르면 그 컷을 바로 끝냄
    private IEnumerator PlayCut(Sequence cut)
    {
        while (cut.IsActive() && !cut.IsComplete())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cut.Complete();
                yield return null;   // 같은 Space 입력이 다음 컷까지 넘기지 않게 한 프레임 쉼
                yield break;
            }
            yield return null;
        }
}

}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SelfBlink : MonoBehaviour
{
    [Header("깜빡임 간격(초)")]
    public float interval = 1f;
    [Header("처음 기다림(초)")]
    public float wait;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        // 오브젝트가 활성화될 때 깜빡임 시작
        StartCoroutine(BlinkRoutine());
    }

    void OnDisable()
    {
        // 비활성화되면 코루틴을 멈춤
        StopAllCoroutines();
        // 확실히 보이게 복구
        if (sr != null) sr.enabled = true;
    }

    IEnumerator BlinkRoutine()
    {
        // 처음에 wait만큼 대기
        if (wait > 0f)
            yield return new WaitForSeconds(wait);

        while (true)
        {
            // Renderer 켜졌다 꺼졌다 반복
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(interval);
        }
    }
}

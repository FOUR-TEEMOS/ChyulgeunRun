using UnityEngine;
using System.Collections;
// This Script is for Managing Sight
//
//    GameObject darker: gets object which makes darker form inspetor
// void sightBothering(): makes window darker
public class SightManager : MonoBehaviour
{
    public GameObject darker;
    public float duration = 3f;
    
    public void sightBothering()
    {
        StartCoroutine(ShowDarkerForSeconds(duration));
    }

    IEnumerator ShowDarkerForSeconds(float duration)
    {
        darker.SetActive(true);  // 나타나게 함
        yield return new WaitForSeconds(duration);  // 3초 대기
        darker.SetActive(false); // 사라지게 함
    }
}

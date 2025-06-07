using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// This Script is for Managing Sight
//
//    GameObject bugs: gets object which makes darker form inspetor
// void sightBothering(): makes window darker
public class SightManager : MonoBehaviour
{
    public List<GameObject> bugs;
    
    public void sightBothering()
    {
        StartCoroutine(ShowBugsSequentially());
    }

    IEnumerator ShowBugsSequentially()
    {
        foreach (var bug in bugs)
            bug.SetActive(true);

        yield return new WaitForSeconds(3f);

        foreach (var bug in bugs)
            bug.SetActive(false);
    }
}

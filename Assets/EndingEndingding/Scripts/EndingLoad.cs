using UnityEngine;
using System.Collections.Generic;

// This Script is for loading ending
//
//       List<Sprite> spriteList : gets each pictures for endings by inspector
// EndingTextLoad endingTextLoad : After EndingLoad gets ending, it also sets ending of endingTextLoad.

public class EndingLoad : MonoBehaviour
{
    public List<Sprite> spriteList;
    public EndingTextLoad endingTextLoad;
    public int debug;
    void Awake()
    {
        endingTextLoad.setEnding(debug);
        int ending = endingTextLoad.getEnding();
        Sprite background = spriteList[ending];
        GetComponent<SpriteRenderer>().sprite = background;
        EndingData.unlockEnding(debug);
    }
    
}

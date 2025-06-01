using UnityEngine;
using System.Collections.Generic;

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
    }
    
}

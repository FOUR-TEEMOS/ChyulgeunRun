using UnityEngine;
using UnityEngine.UI;

public class SetEndingPicture : MonoBehaviour
{
    public Sprite defaul;
    public Sprite origin;

    public Image ending;
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (EndingData.getEndingData(gameObject.name) == 0)
        {
            ending.sprite = defaul;
        }
        else
        {
            ending.sprite = origin;
        }
    }
}

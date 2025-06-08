using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This Script is for managing setting Ending sprites
//
// Sprite defaul : gets a default sprite from inspector
//                 The meaning of a default sprite is the locked Ending sprite
// Sprite origin : gets origin sprites form inspector
//                 The meaning of a origin sprites is the unlocked Ending sprites
//  Image ending : gets Image object from inspector
//                 This is for setting images of this object finally
// void Onclicked() : load Scene EndingScene if this Ending was unlocked when on clicked 
public class SetEndingPicture : MonoBehaviour
{
    public Sprite defaul;
    public Sprite origin;

    public Image ending;
    void Start()
    {
        Debug.Log(EndingData.getEndingData("1"));
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

    public void Onclicked()
    {
        if (EndingData.getEndingData(gameObject.name) == 1)
        {
            PlayerPrefs.SetInt("thisEnding", int.Parse(gameObject.name));
            SceneManager.LoadScene("EndingScene");
        }
        return;
    }
}

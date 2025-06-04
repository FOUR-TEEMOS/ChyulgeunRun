using UnityEngine;

public class EndingData : MonoBehaviour
{
    static public int getEndingData(string i)
    {
        string key = "Ending_" + i;
        int j = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.Save();
        return j;
    }

    static public void unlockEnding(int i)
    {
        string key = "Ending_" + i;
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }
}

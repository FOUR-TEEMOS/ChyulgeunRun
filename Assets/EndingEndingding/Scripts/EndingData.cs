using UnityEngine;

// This Script is for managing Ending Data with playerpref
//
// Warning | there's no scripts for encryping
//
// int getEndingData(string i) : returns whether Ending is locked or not by getting string i which means order of ending
//                               This is static function
//    void unlockEnding(int i) : save unlocked ending information by getting int i which means order of ending
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

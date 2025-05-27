using UnityEngine;

public class EventProgressManager
{
    private const string Key = "CurrentEventIndex";

    public static int GetCurrentEventIndex()
    {
        return PlayerPrefs.GetInt(Key, 2); // EventScene2부터 시작
    }

    public static void AdvanceEventIndex()
    {
        int next = GetCurrentEventIndex() + 1;
        PlayerPrefs.SetInt(Key, next);
        PlayerPrefs.Save();
    }
}


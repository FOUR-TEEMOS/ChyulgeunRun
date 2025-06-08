using UnityEngine;

public static class EventProgressManager
{
    private static int currentEventIndex = -1;

    public static int GetCurrentEventIndex()
    {
        return currentEventIndex;
    }

    public static void SetCurrentEventIndex(int index)
    {
        Debug.Log("[EventProgressManager] SetCurrentEventIndex = " + index);
        currentEventIndex = index;
    }

    public static void MarkEventAsDone(int index)
    {
        PlayerPrefs.SetInt("Event" + index + "_Done", 1);
        PlayerPrefs.Save();
    }
}
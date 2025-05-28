using UnityEngine;
using System.Collections.Generic;

public static class EventProgressManager
{
    private static int currentEventIndex = -1;
    private static List<int> availableEvents = new List<int> { 2, 3, 4, 5, 6 };

    public static bool IsEventDone(int eventIndex)
    {
        return PlayerPrefs.GetInt($"Event{eventIndex}_Done", 0) == 1;
    }

    public static int GetRandomAvailableEvent()
    {
        List<int> notDoneEvents = new List<int>();

        foreach (int e in availableEvents)
        {
            if (!IsEventDone(e)) notDoneEvents.Add(e);
        }

        if (notDoneEvents.Count == 0)
        {
            Debug.Log("모든 이벤트 완료됨");
            return -1;
        }

        int randIndex = Random.Range(0, notDoneEvents.Count);
        currentEventIndex = notDoneEvents[randIndex];
        return currentEventIndex;
    }

    public static int GetCurrentEventIndex()
    {
        return currentEventIndex;
    }

    public static void AdvanceEventIndex()
    {
        if (currentEventIndex != -1)
        {
            PlayerPrefs.SetInt($"Event{currentEventIndex}_Done", 1);
            PlayerPrefs.Save();
            currentEventIndex = -1;
        }
    }

    public static void SetCurrentEventIndex(int index)
    {
        currentEventIndex = index;
    }
}

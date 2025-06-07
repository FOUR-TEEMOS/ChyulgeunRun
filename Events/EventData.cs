using UnityEngine;

[CreateAssetMenu(fileName = "NewEvent", menuName = "Event/ChoiceEvent")]
public class EventData : ScriptableObject
{
    public string eventName;
    public string eventDescription;
    
    [System.Serializable]
    public class Option
    {
        public string optionText;
        public int mentalChange;
        public int timeChange;
        public AudioClip resultSFX;
        public string resultMessage;
    }

    public Option[] options; // 2개 선택지
}

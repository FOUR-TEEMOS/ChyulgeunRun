using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public enum UIType
    {
        Slider,
        Text
    }

    public enum SliderType
    {
        Mental,
        Distance
    }

    [SerializeField] private UIType ui;
    [SerializeField] private SliderType type;
    TextMeshProUGUI myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
        mySlider = GetComponent<Slider>();
    }

    void Update()
    {
        switch (ui)
        {
            case UIType.Text:
                switch (type)
                {
                    case SliderType.Mental:
                        myText.text = string.Format("Mental : {0:F0}", GameManager.Instance.currentMental);
                        break;
                    case SliderType.Distance:
                        myText.text = string.Format("Remain : {0:F0}m", GameManager.Instance.maxDistance - GameManager.Instance.currentDistance);
                        break;
                }
                break;
            case UIType.Slider:
                switch (type)
                {
                    case SliderType.Mental:
                        UpdateMentalBar();
                        break;
                    case SliderType.Distance:
                        UpdateDistanceBar();
                        break;
                }
                break;
        }
    }

    void UpdateMentalBar()
    {
        float currentMental = GameManager.Instance.currentMental;
        float maxMental = GameManager.Instance.maxMental;
        mySlider.value = currentMental / maxMental;
    }

    void UpdateDistanceBar()
    {
        float currentDistance = GameManager.Instance.currentDistance;
        float maxDistance = GameManager.Instance.maxDistance;
        mySlider.value = currentDistance / maxDistance;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public enum SliderType
    {
        Mental,
        Distance
    }

    [SerializeField] private SliderType type;
    Slider mySlider;

    void Awake()
    {
        mySlider = GetComponent<Slider>();
    }

    void Update()
    {
        switch (type)
        {
            case SliderType.Mental:
                UpdateMentalBar();
                break;
            case SliderType.Distance:
                UpdateDistanceBar();
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

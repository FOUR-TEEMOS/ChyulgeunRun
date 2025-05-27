using UnityEngine;
using UnityEngine.UI;

public class MentalUIController : MonoBehaviour
{
    [SerializeField] private Slider mentalBar;

    void Start()
    {
        UpdateMentalUI(GameManager.Instance.currentMental, GameManager.Instance.maxMental);
    }

    public void UpdateMentalUI(float current, float max)
    {
        mentalBar.maxValue = max;
        mentalBar.value = current;
    }
}

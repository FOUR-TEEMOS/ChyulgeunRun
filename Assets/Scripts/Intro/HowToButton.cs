using UnityEngine;
using UnityEngine.UI;

public class HowToButton : MonoBehaviour
{
    public Image panel;

    public void PressButton()
    {
        switch (gameObject.name)
        {
            case "HowToButton":
                panel.gameObject.SetActive(true);
                break;
            case "HowBackButton":
                panel.gameObject.SetActive(false);
                break;
        }
    }
}

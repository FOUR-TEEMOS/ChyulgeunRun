using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider hpSlider;
    private float hp = 0f;
    public float maxHp = 100f;
    public float moveSpeed = 10f;
    public int protection = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (hpSlider == null)
            Debug.Log("Hp 슬라이더가 할당되지 않았습니다.");
    }

    public float Hp
    {
        get { return hp; }
        set { hp = Mathf.Clamp(value, 0f, maxHp); }
        
    }

    void SliderUpdate()
    {
        hpSlider.value = Hp;
        hpSlider.maxValue = maxHp;
    }

    private void Update()
    {
        SliderUpdate();
    }

}

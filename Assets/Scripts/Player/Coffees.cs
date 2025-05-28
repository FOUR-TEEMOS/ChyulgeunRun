using UnityEngine;

public abstract class Coffees : MonoBehaviour
{
    protected float upHp;

    GameObject coffee;

    public abstract void Init();

    private void Awake()
    {
        coffee = this.gameObject;
        coffee.SetActive(true);
        Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.Hp += upHp;
            coffee.SetActive(false);
            Debug.Log($"[smallCoffee °¨Áö] +{upHp}, Hp = {GameManager.instance.Hp}");
        }
            
    }
}

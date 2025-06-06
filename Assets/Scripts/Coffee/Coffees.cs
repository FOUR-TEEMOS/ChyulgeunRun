using UnityEngine;

// 모든 커피들은 이 클래스를 상속받습니다
public abstract class Coffees : MonoBehaviour
{
    protected float UpMental;
    protected coffeeTypes coffeeType;
    protected GameObject coffee;
    
    protected enum coffeeTypes
    {
        SmallCoffee,
        CvCoffee,
        PdCoffee,
        Espresso,
        Coldbrew
    }

    public abstract void Init(); // 오르는 Hp 조절하는 부분
    public abstract void SpecFunction(); // 커피를 먹을 때 커피별로 작동하는 부분

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
            //GameManager.Instance.RecoverMental(UpMental);  //-> itemManager에서 처리 (임시)
            SpecFunction();
            coffee.SetActive(false);
            Debug.Log($"[{coffeeType} 감지] +{UpMental}, Hp = {GameManager.Instance.currentMental}");
        }
            
    }
}

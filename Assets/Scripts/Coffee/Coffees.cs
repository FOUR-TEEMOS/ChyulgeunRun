using UnityEngine;

// 모든 커피들은 이 클래스를 상속받습니다
public abstract class Coffees : MonoBehaviour
{
    protected float UpMental;
    protected coffeeTypes coffeeType;
    protected GameObject coffee;
    [Header("팝업에 띄울 아이콘")]
    public Sprite icon;

    protected enum coffeeTypes
    {
        CvCoffee,
        PdCoffee,
        Espresso,
        Coldbrew
    }

    public abstract void Init(); // 오르는 Hp 조절하는 부분
    public abstract void SpecFunction(); // 커피를 먹을 때 커피별로 작동하는 부분
    public abstract string ExplainDetail(); // 각 능력 커피 설명

    private void Awake()
    {
        coffee = this.gameObject;
        Init();
    }

    public void eatCoffee()
    {
        //GameManager.Instance.RecoverMental(UpMental);  //-> itemManager에서 처리 (임시)
        GameManager.Instance.coffee_DrinkTimes += 1;
        SpecFunction();
    }
}

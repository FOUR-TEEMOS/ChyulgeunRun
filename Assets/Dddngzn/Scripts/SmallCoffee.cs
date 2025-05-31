using UnityEngine;

public class SmallCoffee : Coffees
{
    //정신력 +10 (기본 회복) 
    public override void Init()
    {
        upHp = 10f;
        coffeeType = coffeeTypes.SmallCoffee;
    }

    public override void SpecFunction()
    {
        // 없음
    }
}

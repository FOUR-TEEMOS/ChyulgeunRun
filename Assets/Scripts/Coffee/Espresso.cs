using System.Collections;
using UnityEngine;

public class Espresso : Coffees
{
    //정신력 +5 + 10초 속도 폭주(!주의! 장애물 데미지 두 배)
    public override void Init()
    {
        UpMental = 5f;
        coffeeType = coffeeTypes.Espresso;
    }

    public override void SpecFunction()
    {
        GameManager.Instance.SetSpeedMultiplier(10f);
    }

}

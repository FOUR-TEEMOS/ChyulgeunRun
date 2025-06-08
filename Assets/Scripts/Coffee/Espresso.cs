using System.Collections;
using UnityEngine;

public class Espresso : Coffees
{
    //정신력 +10 + 10초 속도 폭주(!주의! 장애물 데미지 두 배)
    public override void Init()
    {
        UpMental = 10f;
        coffeeType = coffeeTypes.Espresso;
    }

    public override void SpecFunction()
    {
        GameManager.Instance.RecoverMental(UpMental);
        GameManager.Instance.SetSpeedMultiplier(10f, 2f, 0);
    }

    public override string ExplainDetail()
    {
        return "정신력 +10 회복\n10초동안 폭주\n(!주의! 장애물 데미지 2배)";
    }
}

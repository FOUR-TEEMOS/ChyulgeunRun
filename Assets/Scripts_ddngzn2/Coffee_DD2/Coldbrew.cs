using System.Collections;
using UnityEngine;

public class Coldbrew : Coffees
{
    // 정신력 +15 + 다음 선택지 무조건 긍정적 결과 유도
    public override void Init()
    {
        UpMental = 15f;
        coffeeType = coffeeTypes.Coldbrew;
    }

    public override void SpecFunction()
    {
        // 함수 필요
    }

}

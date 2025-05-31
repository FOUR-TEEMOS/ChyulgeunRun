using System.Collections;
using UnityEngine;

public class Espresso : Coffees
{
    //정신력 +5 + 10초 속도 폭주(!주의! 장애물 데미지 두 배)
    public override void Init()
    {
        upHp = 5f;
        coffeeType = coffeeTypes.Espresso;
    }

    public override void SpecFunction()
    {
        StartCoroutine(Timer(10)); // 10초간 속도 폭주
    }

    public IEnumerator Timer(int time) // 시간 동안 이속 2배
    {
        float amount = GameManager.instance.moveSpeed;
        
        GameManager.instance.moveSpeed += amount;
        yield return new WaitForSeconds(time);
        GameManager.instance.moveSpeed -= amount;
    }
}

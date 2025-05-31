using System.Collections;
using UnityEngine;

public class Pdcoffee : Coffees
{
    // 정신력 +20 + 이동 속도 증가
    public override void Init()
    {
        UpMental = 20f;
        coffeeType = coffeeTypes.PdCoffee;
    }

    public override void SpecFunction()
    {
        StartCoroutine(Timer(10)); // 10초간 이속 상승
    }

    public IEnumerator Timer(int time) // 시간 동안 이속 상승
    {
        GameManager.Instance.moveSpeed += 10f;
        yield return new WaitForSeconds(time);
        GameManager.Instance.moveSpeed -= 10f;
    }
}

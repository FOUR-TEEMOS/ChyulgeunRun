using System.Collections;
using UnityEngine;

public class Pdcoffee : Coffees
{
    private ItemSpawner itemSpawner;

    // 정신력 +20 + 장애물 생성 정지
    public override void Init() // Awake에서 처리됨
    {
        itemSpawner = GameObject.Find("Spawner").GetComponent<ItemSpawner>();
        UpMental = 15f;
        coffeeType = coffeeTypes.PdCoffee;
    }

    public override void SpecFunction()
    {
        GameManager.Instance.RecoverMental(UpMental);
        itemSpawner.BlockSpawn(10f); // 10초동안 장애물 생성 정지
    }

    public override string ExplainDetail()
    {
        return "정신력 +15 회복\n장애물 생성 일시정지";
    }

}

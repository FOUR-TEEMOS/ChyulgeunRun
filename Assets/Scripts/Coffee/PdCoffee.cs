using System.Collections;
using UnityEngine;

public class Pdcoffee : Coffees
{
    private ItemSpawner itemSpawner;


    // 정신력 +20 + 장애물 생성 정지
    public override void Init() // Awake에서 처리됨
    {
        itemSpawner = GameObject.Find("Spawner").GetComponent<ItemSpawner>();
        UpMental = 20f;
        coffeeType = coffeeTypes.PdCoffee;
    }

    private void Awake()
    {
        coffee = this.gameObject;
        coffee.SetActive(true);
        Init();
    }

    public override void SpecFunction()
    {
        itemSpawner.BlockSpawn(5f); // 5초동안 장애물 생성 정지
    }

}

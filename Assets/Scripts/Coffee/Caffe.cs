using System.Collections.Generic;
using UnityEngine;

public class Caffe : MonoBehaviour
{
    [Header("능력 커피 목록")]
    public List<Coffees> coffees;

    public bool hasTriggerd = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (hasTriggerd) return;
    
        // 리스트에서 랜덤으로 하나 선택
        int idx = Random.Range(0, coffees.Count);
        Coffees selectedCoffee = coffees[idx];


        Debug.Log($"[Caffe] 획득한 커피: {selectedCoffee.name}");

        // 팝업 창 띄우기
        CoffeePopup.Instance.Show(selectedCoffee);
        hasTriggerd = true;
    }
}

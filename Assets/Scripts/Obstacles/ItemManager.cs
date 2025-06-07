using UnityEngine;

// This Script is for managing Items
//
// COLLISION: When items collide with the object with tag 'disappear', they would all disappear
//            For RECOVERY items, when they collide with the object with tag 'Player', they would all disappear                              
//                                which means the Player gets these items.
//                              
// getAmount(): returns Damage of Obstacles (Negative) or Amount of Recovery items (Positive)
//              This function refers from ItemDataManager

//              Warning || The Object with this script must have exact NAME in Item Lists
//                              Item Lists |        Obstacles - barricade, bugs, do, puddle
//                                         |   Recovery items - coffee


public class ItemManager : MonoBehaviour
{
    private SightManager sightManager;

    private void Awake()
    {
        sightManager = GameObject.Find("SightManager").GetComponent<SightManager>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("disappear"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            int amount = getAmount();
            if (gameObject.CompareTag("recovery"))
            {
                GameManager.Instance.RecoverMental(amount);
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("obstacle"))
            {
                if (gameObject.name == "do(Clone)" || gameObject.name == "somae(Clone)")
                {
                    Debug.Log("패링 중 -> ItemManager는 무시, PlayerController에서 처리");
                    return;
                }

                if (GameManager.Instance.protection == true ||
                    GameManager.Instance.superProtection == true)
                {
                    GameManager.Instance.protection = false;
                    Debug.Log($"protection 성공");
                    return;
                }

                if (gameObject.name == "bugs(Clone)")
                {
                    sightManager.CoverWithBugs();
                }
                if (gameObject.name == "jeondan(Clone)")
                {
                    sightManager.CoverWithFlyers();
                }
                if (gameObject.name == "puddle(Clone)" && SelectionTriggerManager.Instance.RainyUmbrellaTimer() > 0f)
                {
                    amount *= 2;
                }
                Debug.Log("ItemManager에서 데미지");
                GameManager.Instance.TakeMentalDamage(amount);
            }
        }
    }

    public int getAmount()
    {
        return ItemDataManager.getAmount(gameObject.name);
    }
}
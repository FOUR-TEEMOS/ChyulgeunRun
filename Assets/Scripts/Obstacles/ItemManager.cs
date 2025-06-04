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
    private ItemDataManager ItemDataManager;
    private SightManager sightManager;


    private void Awake()
    {
        ItemDataManager = GameObject.Find("ItemDataManager").GetComponent<ItemDataManager>();
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
                PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
                if (pc != null && pc.IsParrying())
                {
                    Debug.Log("패링 중 → ItemManager는 무시, PlayerController에서 처리");
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
                    sightManager.sightBothering();
                }
                GameManager.Instance.TakeMentalDamage(amount);
            }
        }
    }

    public int getAmount()
    {
        return ItemDataManager.getAmount(gameObject.name);
    }
}
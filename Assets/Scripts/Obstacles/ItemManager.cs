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
                if(GameManager.Instance.protection > 0)
                { // protection 있을 경우 패스
                    
                    GameManager.Instance.protection -= 1;
                    Debug.Log($"프로텍션 소모. 현재 {GameManager.Instance.protection}");
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
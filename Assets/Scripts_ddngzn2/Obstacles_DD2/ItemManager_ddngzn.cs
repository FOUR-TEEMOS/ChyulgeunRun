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
<<<<<<<< HEAD:Assets/KMJ/Scripts/Obstacles/ItemManager.cs
    
    public void Start()
========


    private void Awake()
>>>>>>>> origin/Ddddddddddngzn2:Assets/Scripts_ddngzn2/Obstacles_DD2/ItemManager_ddngzn.cs
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
<<<<<<<< HEAD:Assets/KMJ/Scripts/Obstacles/ItemManager.cs
                    Debug.Log("íŒ¨ë§ ì¤‘ â†’ ItemManagerëŠ” ë¬´ì‹œ, PlayerControllerì—ì„œ ì²˜ë¦¬");
                    return;
                }
                if (GameManager.Instance.protection == true)
                {
                    GameManager.Instance.protection = false;
                    Debug.Log($"protection ì„±ê³µ");
                    return;
                }
========
                    Debug.Log("ÆÐ¸µ Áß ¡æ ItemManager´Â ¹«½Ã, PlayerController¿¡¼­ Ã³¸®");
                    return;
                }
                if (GameManager.Instance.protection == true ||
                    GameManager.Instance.superProtection == true)
                {
                    GameManager.Instance.protection = false;
                    Debug.Log($"protection ¼º°ø");
                    return;
                }

>>>>>>>> origin/Ddddddddddngzn2:Assets/Scripts_ddngzn2/Obstacles_DD2/ItemManager_ddngzn.cs
                if (gameObject.name == "bugs(Clone)")
                {
                    sightManager.sightBothering();
                }
                if (gameObject.name == "puddle(Clone)" && SelectionTriggerManager.Instance.RainyUmbrellaTimer() > 0f)
                {
                    amount *= 2;
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
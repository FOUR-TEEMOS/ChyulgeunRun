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
    public void Start()
    {
        ItemDataManager = GameObject.Find("ItemDataManager").GetComponent<ItemDataManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("disappear"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("recovery"))
        {
            Destroy(gameObject);
        }
    }

    public int getAmount()
    {
        return ItemDataManager.getAmount(gameObject.name);
    }
}
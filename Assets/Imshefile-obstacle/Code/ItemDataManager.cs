using UnityEngine;

// This Script is for managing Item Datas
//
// getAmount(string item_name): returns Damage of Obstacles (Negative) or Amount of Recovery items (Positive)
//                              and it requires exact NAMES of the Sprite
//                              
//                              Item Lists |        Obstacles - barricade, bugs, do, puddle
//                                         |   Recovery items - coffee
public class ItemDataManager : MonoBehaviour
{
    public int getAmount(string item_name)
    {
         switch (item_name)
        {
            // obstacles
            case "barricade":
                return -1;
            case "bugs":
                return -1;
            case "do":
                return -1;
            case "puddle":
                return -1;
            // recovery
            case "coffee":
                return +1;
        }
        return 0;
    }
}

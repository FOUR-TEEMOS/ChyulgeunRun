using UnityEngine;

// This Script is for managing Item Datas
//
// getAmount(string item_name): returns Damage of Obstacles (Negative) or Amount of Recovery items (Positive)
//                              and it requires exact NAMES of the Sprite
//                              
//                              Item Lists |        Obstacles - barricade, bugs, do, pickpocket, puddle
//                                         |   Recovery items - vmcoffee, cvcoffee, pdcoffee, espresso, dclatte, coldbrew
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
            case "pickpocket":
                return -1;
            case "puddle":
                return -1;
            // recovery
            case "vmcoffee":
                return +1;
            case "cvcoffe":
                return +1;
            case "pdcoffee":
                return +1;
            case "espresso":
                return +1;
            case "dclatte":
                return +1;
            case "coldbrew":
                return +1;
        }
        return 0;
    }
}

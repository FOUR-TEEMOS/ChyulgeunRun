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
            case "barricade(Clone)":
                return 5;
            case "bugs(Clone)":
                return 5;
            case "do(Clone)":
                return 5;
            case "pickpocket(Clone)":
                return 5;
            case "puddle(Clone)":
                return 5;
            case "jeondan(Clone)":
                return 5;
            // recovery
            case "vmcoffee(Clone)":
                return 3;
            case "cvcoffe(Clone)":
                return 3;
            case "pdcoffee(Clone)":
                return 1;
            case "espresso(Clone)":
                return 1;
            case "dclatte(Clone)":
                return 1;
            case "coldbrew(Clone)":
                return 1;
        }
        return 0;
    }
}

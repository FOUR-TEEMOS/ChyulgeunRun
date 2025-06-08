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

    //이름으로 패링장애물 여부 검색.
    public static int getObsType(string item_name)
    {
        switch (item_name)
        {
            // 패링
            case "do(Clone)":
                return 1;
            case "do":
                return 1;
            case "somae(Clone)":
                return 1;
            case "somae":
                return 1;
            default:
                return 0;
        }
    }

    public static int getAmount(string item_name)
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
            case "trash(Clone)":
                return 5;
            case "hole(Clone)":
                return 5;
            case "somae(Clone)":
                return 5;
            // recovery
            case "cvcoffe(Clone)":
                return 3;
            case "pdcoffee(Clone)":
                return 1;
            case "espresso(Clone)":
                return 1;
        }
        return 0;
    }
}

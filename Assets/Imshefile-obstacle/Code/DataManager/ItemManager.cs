using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int getAmount(string item_name) // obstacle 데미지 및 아이템 회복량
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
            // coffees
            case "coffee":
                return 1;
        }
        return 0;
    }
}

using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public int getDamage(string obstacle_name)
    {
         switch (obstacle_name)
        {
            case "barricade":
                return 1;
            case "bugs":
                return 1;
            case "do":
                return 1;
            case "puddle":
                return 1;
        }
        return 0;
    }
}

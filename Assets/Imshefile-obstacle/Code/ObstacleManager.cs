using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ItemManager ItemManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("disappear"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("item"))
        {
            Destroy(gameObject);
        }
    }

    public int getAmount()
    {
        return ItemManager.getAmount(gameObject.name);
    }
}
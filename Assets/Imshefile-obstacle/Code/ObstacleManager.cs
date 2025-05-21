using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public DamageManager DamageManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("disappear"))
        {
            Destroy(gameObject);
        }
    }

    public int getDamage()
    {
        return DamageManager.getDamage(gameObject.name);
    }
}
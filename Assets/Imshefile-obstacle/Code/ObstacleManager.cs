using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("disappear"))
        {
            Destroy(gameObject);
        }
    }
}
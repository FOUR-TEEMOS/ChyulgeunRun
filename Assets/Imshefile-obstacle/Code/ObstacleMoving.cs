using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    public float speed;

    void Update()
    { 
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}

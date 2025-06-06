using UnityEngine;

// This Script moves items + ес
//
// float speed: gets speed of movement of items + ес from inspector
//              Movement of items + ес is proportional to the 'speed'
//              The original setting is 4f
public class Moving : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    { 
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}

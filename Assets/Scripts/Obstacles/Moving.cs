using UnityEngine;

// This Script moves items + α
//
// float speed: gets speed of movement of items + α from inspector
//              Movement of items + α is proportional to the 'speed'
//              The original setting is 4f
public class Moving : MonoBehaviour
{
    [Header("현재 속도")]
    public float speed = 4f;
    [Header("속도 기본값")]
    public float defaultSpeed = 4f;

    void Update()
    {
        speed = GameManager.Instance.GetSpeedMultiplier() * defaultSpeed; // 속도를 계속해서 갱신.
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}

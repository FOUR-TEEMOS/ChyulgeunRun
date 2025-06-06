using UnityEngine;

public class DoObstacle : MonoBehaviour
{
    public float warningDistance = 3f;

    private Transform player;
    private PlayerController playerController;
    private bool warningGiven = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        // 플레이어와 일정 거리 이내 접근 시 반격 준비
        if (!warningGiven && Vector2.Distance(transform.position, player.position) < warningDistance)
        {
            playerController.ShowParryWarning();
            playerController.StartParry();
            warningGiven = true;
        }
    }
}

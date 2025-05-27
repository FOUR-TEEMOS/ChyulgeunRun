using UnityEngine;

public class DoObstacle : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float warningDistance = 3f;

    private Transform player;
    private PlayerController playerController;
    private bool warningGiven = false;
    public float destroyX = -15f; // 화면 바깥 기준 좌표

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();

        // 화면 왼쪽 밖으로 나가면 제거
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 왼쪽으로 이동 (달려오는 느낌)
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // 플레이어와 일정 거리 이내 접근 시 반격 준비
        if (!warningGiven && Vector2.Distance(transform.position, player.position) < warningDistance)
        {
            playerController.ShowParryWarning();
            playerController.StartParry();
            warningGiven = true;
        }
    }
}

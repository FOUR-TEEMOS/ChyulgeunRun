using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpForce = 7f;
    public float slideDuration = 0.5f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;

    private bool isGrounded = true;
    private bool isSliding = false;
    private float slideTimer = 0f;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private Vector2 slideColliderSize = new Vector2(1.35f, 0.7f);  // 슬라이드 시 크기
    private Vector2 slideColliderOffset = new Vector2(0.015f, -0.5f); // 슬라이드 시 위치

    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        // 현재 콜라이더의 원래 크기 저장
        originalColliderSize = coll.size;
        originalColliderOffset = coll.offset;
    }

    void Update()
    {
        // 점프 (C 키)
        if (Input.GetKeyDown(KeyCode.C) && isGrounded && !isSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        // 슬라이드 (Z 키)
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded && !isSliding)
        {
            isSliding = true;
            slideTimer = slideDuration;

            // 콜라이더 작게 변경
            coll.size = slideColliderSize;
            coll.offset = slideColliderOffset;

            // TODO: 슬라이드 애니메이션 재생
        }

        // 슬라이드 타이머
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0f)
            {
                isSliding = false;

                // 콜라이더 원래대로 복구
                coll.size = originalColliderSize;
                coll.offset = originalColliderOffset;

                // TODO: 달리기 애니메이션 재생
            }
        }

        // 반격 대응 (X 키)
        if (Input.GetKeyDown(KeyCode.X))
        {
            // TODO: 타이밍 맞으면 성공, 아니면 실패 처리
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥인지 확인
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            // 아래쪽 충돌만 감지
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y >= -0.35f)
                {
                    isGrounded = true;

                    // 착지 : y속도 제거
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 떨어졌을 때
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
}

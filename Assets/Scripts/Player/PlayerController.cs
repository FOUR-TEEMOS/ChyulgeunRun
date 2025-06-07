using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private Animator anim;

    // 점프 & 슬라이딩 관련
    [SerializeField] LayerMask groundLayer;

    public float jumpForce = 7f;
    public float slideDuration = 0.5f;

    private bool isGrounded = true;
    private bool isSliding = false;
    private bool isCaught = false;

    private Vector3 originalPos;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private Vector2 slideColliderSize = new Vector2(1.35f, 1.9f); // 슬라이드 시 크기
    private Vector2 slideColliderOffset = new Vector2(0f, 0f); // 슬라이드 시 위치

    // 패링 관련
    private bool isParrying = false;
    private bool hasParried = false;
    public float parryDuration = 0.4f;
    private float parryTimer = 0f;
    private bool canParryInput = true;
    public float xCooldownTimer = 0f;
    public float xCooldown = 2f;  // X키 쿨타임 (연타 방지)


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        originalPos = transform.position;
        // 현재 콜라이더의 원래 크기 저장
        originalColliderSize = coll.size;
        originalColliderOffset = coll.offset;
    }

    void Update()
    {
        // 점프 (C 키)
        if (Input.GetKeyDown(KeyCode.C) && isGrounded && !isSliding && !isCaught)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        // 슬라이드 (Z 키)
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded && !isSliding && !isCaught && !isParrying)
        {
            isSliding = true;
        
            transform.position = new Vector2(-6.3f, -3f);
            // 콜라이더 작게 변경
            coll.size = slideColliderSize;
            coll.offset = slideColliderOffset;

            anim.SetBool("Sliding", true);
        }

        // 슬라이드 종료 (Z 키에서 손 뗐을 때)
        if (Input.GetKeyUp(KeyCode.Z) && isSliding && !isCaught && !isParrying)
        {
            isSliding = false;

            transform.position = originalPos;
            // 콜라이더 원래대로 복구
            coll.size = originalColliderSize;
            coll.offset = originalColliderOffset;

            anim.SetBool("Sliding", false);
        }

        // 반격 대응 (X 키)
        if (isParrying && !hasParried && canParryInput && Input.GetKeyDown(KeyCode.X) && !isCaught)
        {
            hasParried = true;
            ParrySuccess();
        }

        // 반격 시간 체크
        if (isParrying)
        {
            parryTimer -= Time.deltaTime;
            if (parryTimer <= 0f && !hasParried)
            {
                if (GameManager.Instance.protection == true ||
                    GameManager.Instance.superProtection == true)
                {
                    Debug.Log("protection으로 패링 방어");
                    GameManager.Instance.protection = false;
                    isParrying = false;
                    hasParried = false;
                }
                else
                    ParryFail();
            }
        }

        if (Input.GetKeyDown(KeyCode.X) && canParryInput && !isCaught)
        {
            canParryInput = false;
            xCooldownTimer = xCooldown;
            Debug.Log("X 키 입력됨 (쿨타임 시작)");
        }

        // X 키 쿨타임
        if (!canParryInput && xCooldownTimer > 0f)
        {
            xCooldownTimer -= Time.deltaTime;
            if (xCooldownTimer <= 0f)
            {
                canParryInput = true;
                Debug.Log("X 키 쿨타임 끝");
            }
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle") && collision.gameObject.name != ("do(Clone)"))
        {
            anim.SetTrigger("Damaged");
        }
    }

    // 반격 타이밍 시작
    public void StartParry()
    {
        if (isSliding)
        {
            isSliding = false;
            // 콜라이더 복구
            coll.size = originalColliderSize;
            coll.offset = originalColliderOffset;
            // 위치 복구
            transform.position = originalPos;
            // 애니메이터도 false
            anim.SetBool("Sliding", false);
        }

        isParrying = true;
        hasParried = false;
        parryTimer = parryDuration;

        anim.SetBool("Caution", true);
        Debug.Log("반격 준비 중!");
    }

    public bool IsParrying()
    {
        if (isParrying == true) return true;
        return false;
    }

    public void ParrySuccess()
    {
        isParrying = false;
        hasParried = false;
        canParryInput = false;
        xCooldownTimer = xCooldown;

        anim.SetBool("ParryingSuccess", true); 
        anim.SetBool("Caution", false);
        Debug.Log("반격 성공!");
    }

    public void ParryFail()
    {
        isParrying = false;
        hasParried = false;
        canParryInput = false;
        xCooldownTimer = xCooldown;

        anim.SetBool("Caution", false);
        anim.SetBool("ParryingSuccess", false);
        isCaught = true;
        GameManager.Instance.caught(3f);

        StartCoroutine(ResetCaught());

        int amount = ItemDataManager.getAmount("do(Clone)");
        GameManager.Instance.TakeMentalDamage(amount);
        Debug.Log("반격 실패...");
    }

    private IEnumerator ResetCaught()
    {
        yield return new WaitForSeconds(3f);  // 붙잡힘 시간
        anim.SetBool("ParryingSuccess", true);
        isCaught = false;
    }
}



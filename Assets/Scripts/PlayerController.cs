using UnityEngine;

public enum Controls { mobile, pc }

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float doubleJumpForce = 8f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;

    [Header("References")]
    public Animator playeranim;
    public ParticleSystem footsteps;
    public ParticleSystem ImpactEffect;

    [Header("Mode")]
    public Controls controlmode;

    [Header("State")]
    public bool isPaused = false;

    Rigidbody2D rb;
    bool isGroundedBool = false;
    bool canDoubleJump = false;
    bool wasOnGround = false;

    float moveX;

    ParticleSystem.EmissionModule footEmissions;

    [Header("Shooting")]
    public float fireRate = 0.5f;
    float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // optional auto-find
        if (playeranim == null) playeranim = GetComponentInChildren<Animator>();

        if (footsteps != null)
            footEmissions = footsteps.emission;
        else
            Debug.LogWarning("PlayerController: footsteps холбоогүй байна.", this);

        if (ImpactEffect == null)
            Debug.LogWarning("PlayerController: ImpactEffect холбоогүй байна.", this);

        if (groundCheck == null)
            Debug.LogError("PlayerController: groundCheck холбоогүй байна!", this);

        if (controlmode == Controls.mobile && UIManager.instance != null)
            UIManager.instance.EnableMobileControls();
    }

    void Update()
    {
        isGroundedBool = IsGrounded();

        if (isGroundedBool)
            canDoubleJump = true;

        // PC input
        if (controlmode == Controls.pc)
            moveX = Input.GetAxis("Horizontal");

        // Jump input (PC)
        if (controlmode == Controls.pc && Input.GetButtonDown("Jump"))
        {
            if (isGroundedBool)
            {
                Jump(jumpForce);
            }
            else if (canDoubleJump)
            {
                Jump(doubleJumpForce);
                canDoubleJump = false;
            }
        }

        // Shoot (PC)
        if (controlmode == Controls.pc && !isPaused && Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        SetAnimations();

        if (moveX != 0)
            FlipSprite(moveX);

        // Impact effect on landing
        if (!wasOnGround && isGroundedBool && ImpactEffect != null && footsteps != null)
        {
            ImpactEffect.gameObject.SetActive(true);
            ImpactEffect.Stop();
            ImpactEffect.transform.position = new Vector2(
                footsteps.transform.position.x,
                footsteps.transform.position.y - 0.2f
            );
            ImpactEffect.Play();
        }

        wasOnGround = isGroundedBool;
    }

    void FixedUpdate()
    {
        if (rb == null) return;
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    void Jump(float force)
    {
        if (rb == null) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        if (playeranim != null)
            playeranim.SetTrigger("jump");
    }

    bool IsGrounded()
    {
        if (groundCheck == null) return false;

        float rayLength = 0.25f;
        Vector2 origin = new Vector2(groundCheck.position.x, groundCheck.position.y - 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }

    void SetAnimations()
    {
        if (playeranim == null) return;

        bool running = (moveX != 0 && isGroundedBool);

        playeranim.SetBool("run", running);
        playeranim.SetBool("isGrounded", isGroundedBool);

        if (footsteps != null)
            footEmissions.rateOverTime = running ? 35f : 0f;
    }

    void FlipSprite(float direction)
    {
        transform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("killzone") && GameManager.instance != null)
            GameManager.instance.Death();
    }

    // -------------------------
    // Mobile controls
    // -------------------------
    public void MobileMove(float value)
    {
        moveX = value;
    }

    public void MobileJump()
    {
        if (isGroundedBool)
        {
            Jump(jumpForce);
        }
        else if (canDoubleJump)
        {
            Jump(doubleJumpForce);
            canDoubleJump = false;
        }
    }

    public void MobileShoot()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public void Shoot()
    {
        // projectile logic эндээ
    }
}

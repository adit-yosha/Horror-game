using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.5f;

    private Rigidbody2D rb;
    private Vector2 input;
    private bool isFrozen = false;

    private Animator anim;

    // === SOUND ===
    public AudioSource footstepSource;

    // 🔥 SIMPAN ARAH TERAKHIR
    public Vector2 LastDirection { get; private set; } = Vector2.down;

    // =====================
    // 🔥 TAMBAHAN DEATH SYSTEM
    // =====================
    public Transform spawnPoint;
    private bool isDead = false;

    // 🔥 TAMBAHAN
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        rb.gravityScale = 0;
        rb.freezeRotation = true;

        if (footstepSource == null)
            footstepSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isFrozen || isDead)
        {
            input = Vector2.zero;
            UpdateAnimator();
            HandleFootstep();
            return;
        }

        // 🔥 UPDATE ARAH
        if (input.magnitude > 0.01f)
        {
            LastDirection = input.normalized;
        }

        UpdateAnimator();
        HandleFootstep();
    }

    void FixedUpdate()
    {
        if (isFrozen || isDead) return;

        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }

    // =====================
    // MOBILE INPUT
    // =====================
    public void MoveUp(bool press)
    {
        input.y = press ? 1 : 0;
    }

    public void MoveDown(bool press)
    {
        input.y = press ? -1 : 0;
    }

    public void MoveLeft(bool press)
    {
        input.x = press ? -1 : 0;
    }

    public void MoveRight(bool press)
    {
        input.x = press ? 1 : 0;
    }

    // =====================
    // ANIMATOR
    // =====================
    void UpdateAnimator()
    {
        bool walking = input.magnitude > 0.01f;

        anim.SetBool("IsWalking", walking);

        if (walking)
        {
            anim.SetFloat("InputX", input.x);
            anim.SetFloat("InputY", input.y);
        }
    }

    // =====================
    // FOOTSTEP
    // =====================
    void HandleFootstep()
    {
        if (input.magnitude > 0.1f)
        {
            if (!footstepSource.isPlaying)
                footstepSource.Play();
        }
        else
        {
            footstepSource.Stop();
        }
    }

    // =====================
    // FREEZE
    // =====================
    public void Freeze(float duration)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(duration);

        isFrozen = false;
    }

    // =====================
    // 🔥 DETECT ENEMY (FIX)
    // =====================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Kena: " + collision.name); // 🔥 DEBUG

        if (collision.CompareTag("Ghost") && !isDead)
        {
            isDead = true; // 🔥 PINDAH KE SINI (ANTI DOUBLE TRIGGER)
            StartCoroutine(DieAndRespawn());
        }
    }

    // =====================
    // 💀 DIE + RESPAWN (FIX)
    // =====================
    private IEnumerator DieAndRespawn()
    {
        // 🔥 MATIKAN COLLIDER (ANTI NYENTUH TERUS)
        col.enabled = false;

        // stop gerak total
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // delay biar ada efek mati
        yield return new WaitForSeconds(0.5f);

        // pindah ke spawn
        transform.position = spawnPoint.position;

        // kasih waktu aman
        yield return new WaitForSeconds(0.3f);

        // 🔥 HIDUPKAN LAGI COLLIDER
        col.enabled = true;

        isDead = false;
    }
}
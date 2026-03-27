using UnityEngine;

public class GhostPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float waitTime = 1f;

    public float chaseRange = 5f;
    public float chaseSpeed = 3f;

    public LayerMask wallLayer;

    int currentPoint = 0;
    float waitCounter;

    Transform player;
    Rigidbody2D rb;

    // 🔥 TAMBAHAN
    bool isTouchingPlayer = false;

    void Start()
    {
        waitCounter = waitTime;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (player == null || patrolPoints.Length == 0) return;

        // 🔥 STOP kalau lagi nyentuh player
        if (isTouchingPlayer) return;

        Vector2 origin = rb.position;
        Vector2 target = player.position;

        float distanceToPlayer = Vector2.Distance(origin, target);

        bool canSeePlayer = false;

        if (distanceToPlayer <= chaseRange)
        {
            RaycastHit2D hit = Physics2D.Linecast(origin, target, wallLayer);

            if (hit.collider == null)
                canSeePlayer = true;
        }

        if (canSeePlayer)
        {
            // ===== CHASE PLAYER =====
            Vector2 dir = (target - origin).normalized;
            rb.MovePosition(origin + dir * chaseSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // ===== PATROL =====
            Transform point = patrolPoints[currentPoint];

            Vector2 newPos = Vector2.MoveTowards(
                origin,
                point.position,
                speed * Time.fixedDeltaTime
            );

            rb.MovePosition(newPos);

            if (Vector2.Distance(origin, point.position) < 0.1f)
            {
                if (waitCounter <= 0)
                {
                    currentPoint++;

                    if (currentPoint >= patrolPoints.Length)
                        currentPoint = 0;

                    waitCounter = waitTime;
                }
                else
                {
                    waitCounter -= Time.fixedDeltaTime;
                }
            }
        }

        Debug.DrawLine(origin, target, Color.red);
    }

    // =====================
    // 🔥 DETECT PLAYER
    // =====================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }
}
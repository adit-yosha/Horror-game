using UnityEngine;

public class GhostJumpScare : MonoBehaviour
{
    public Transform player;
    public float speed = 18f;
    public float stopDistance = 0.5f;

    private bool active = true;

    void Update()
    {
        if (!active || player == null) return;

        Vector2 dir = (player.position - transform.position);
        float distance = dir.magnitude;

        if (distance <= stopDistance)
        {
            active = false; // berhenti biar ga tembus player
            return;
        }

        transform.position += (Vector3)(dir.normalized * speed * Time.deltaTime);
    }
}

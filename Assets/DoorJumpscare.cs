using UnityEngine;

public class DoorJumpScare : MonoBehaviour
{
    public GameObject ghost;
    public PlayerMovement player;
    public CameraShake cameraShake;

    private bool triggered = false;

    void Start()
    {
        ghost.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            ghost.SetActive(true);

            // Freeze player 0.5 detik
            player.Freeze(0.5f);

            // Camera shake
            cameraShake.Shake(0.4f, 0.3f);
        }
    }
}

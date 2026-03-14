using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    public bool isLocked = false;
    public bool startOpen = false;
    public float openAngle = 90f;
    public float openSpeed = 5f;

    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.E;
    public Transform pivot; // titik engsel pintu

    [Header("Optional")]
    public AudioSource openSound;
    public AudioSource lockedSound;

    private bool isOpen;
    private bool playerNearby;
    private Quaternion closedRot;
    private Quaternion openRot;

    void Start()
    {
        if (pivot == null)
            pivot = transform;

        closedRot = pivot.rotation;
        openRot = Quaternion.Euler(0, 0, openAngle) * closedRot;

        isOpen = startOpen;
        pivot.rotation = isOpen ? openRot : closedRot;
    }

    void Update()
    {
        if (!playerNearby) return;

        if (Input.GetKeyDown(interactKey))
        {
            TryToggleDoor();
        }

        // animasi halus
        Quaternion targetRot = isOpen ? openRot : closedRot;
        pivot.rotation = Quaternion.Lerp(
            pivot.rotation,
            targetRot,
            Time.deltaTime * openSpeed
        );
    }

    void TryToggleDoor()
    {
        if (isLocked)
        {
            if (lockedSound) lockedSound.Play();
            return;
        }

        isOpen = !isOpen;

        if (openSound)
            openSound.Play();
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}

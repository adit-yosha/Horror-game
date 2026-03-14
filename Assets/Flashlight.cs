using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    public Light2D flashlight;
    public PlayerMovement player;

    public float distance = 0.4f;

    void Start()
    {
        if (flashlight == null)
            flashlight = GetComponent<Light2D>();

        flashlight.enabled = false;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 dir = player.LastDirection;

        if (dir != Vector2.zero)
        {
            transform.localPosition = dir.normalized * distance;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    // 🔥 DIPANGGIL DARI BUTTON
    public void ToggleFlashlight()
    {
        flashlight.enabled = !flashlight.enabled;
    }
}

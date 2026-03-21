using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    public Light2D light2D;
    public PlayerMovement player;

    public float distance = 0.5f;

    void Update()
    {
        if (player == null || light2D == null) return;

        Vector2 dir = player.LastDirection;

        // Kalau player lagi diam, jangan ubah arah
        if (dir.magnitude < 0.1f) return;

        // 🔥 POSISI di depan player
        transform.localPosition = dir.normalized * distance;

        // 🔥 ROTASI mengikuti arah
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    // 🔥 ON / OFF flashlight
    public void ToggleFlashlight()
    {
        light2D.enabled = !light2D.enabled;
    }
}
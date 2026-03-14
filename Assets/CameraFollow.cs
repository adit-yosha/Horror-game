using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    Camera cam;
    float camHalfHeight;
    float camHalfWidth;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateCameraSize();
    }

    void LateUpdate()
    {
        if (target == null) return;

        UpdateCameraSize(); // 🔥 penting untuk mobile

        Vector3 desiredPosition = target.position + offset;

        float clampX = Mathf.Clamp(
            desiredPosition.x,
            minBounds.x + camHalfWidth,
            maxBounds.x - camHalfWidth
        );

        float clampY = Mathf.Clamp(
            desiredPosition.y,
            minBounds.y + camHalfHeight,
            maxBounds.y - camHalfHeight
        );

        Vector3 clampedPos = new Vector3(clampX, clampY, desiredPosition.z);

        transform.position = Vector3.Lerp(
            transform.position,
            clampedPos,
            smoothSpeed * Time.deltaTime
        );
    }

    void UpdateCameraSize()
    {
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }
}

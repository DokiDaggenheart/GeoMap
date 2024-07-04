using UnityEngine;
using Cinemachine;

public class CameraBounds : MonoBehaviour
{
    public Vector2 minBounds; // ћинимальные координаты границ камеры
    public Vector2 maxBounds; // ћаксимальные координаты границ камеры
    private CinemachineVirtualCamera virtualCamera;
    private Transform playerTransform;
    private Camera mainCamera;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        playerTransform = virtualCamera.Follow;
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 desiredPosition = playerTransform.position;

            // ѕолучаем размеры видимого пространства камеры
            float halfHeight = mainCamera.orthographicSize;
            float halfWidth = mainCamera.aspect * halfHeight;

            // ќграничиваем позицию камеры в пределах границ
            float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

            // ”станавливаем позицию камеры
            virtualCamera.transform.position = new Vector3(clampedX, clampedY, virtualCamera.transform.position.z);
        }
    }
}
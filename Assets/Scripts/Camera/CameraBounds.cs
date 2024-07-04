using UnityEngine;
using Cinemachine;

public class CameraBounds : MonoBehaviour
{
    public Vector2 minBounds; // ����������� ���������� ������ ������
    public Vector2 maxBounds; // ������������ ���������� ������ ������
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

            // �������� ������� �������� ������������ ������
            float halfHeight = mainCamera.orthographicSize;
            float halfWidth = mainCamera.aspect * halfHeight;

            // ������������ ������� ������ � �������� ������
            float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

            // ������������� ������� ������
            virtualCamera.transform.position = new Vector3(clampedX, clampedY, virtualCamera.transform.position.z);
        }
    }
}
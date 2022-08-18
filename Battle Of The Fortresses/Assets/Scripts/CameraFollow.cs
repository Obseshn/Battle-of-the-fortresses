using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings:")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;
    [SerializeField] private Transform targetTransform;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 desiredPosition;

    private void LateUpdate()
    {
        desiredPosition = targetTransform.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothTime);
    }
}

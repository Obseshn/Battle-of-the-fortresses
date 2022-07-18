using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings:")]
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothTime;
    [SerializeField] private Transform _targetTransform;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 desiredPosition;

    private void LateUpdate()
    {
        desiredPosition = _targetTransform.position + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothTime);
    }
}

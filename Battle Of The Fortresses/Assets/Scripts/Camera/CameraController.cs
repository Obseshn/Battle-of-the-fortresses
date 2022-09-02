using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings:")]
    [SerializeField] private Vector3 baseOffset;
    [SerializeField] private Vector3 additionalOffset;
    [SerializeField] private float smoothTime;
    [SerializeField] private Transform targetTransform;

    private Vector3 velocity = Vector3.zero;
    private Vector3 desiredPosition;

    private void Start()
    {
        ArmyCommander.OnCountOfUnitsChange += ChangeOffsetByCountOfUnits;
    }
    private void LateUpdate()
    {

        desiredPosition = targetTransform.position + (baseOffset + additionalOffset);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

    private void ChangeOffsetByCountOfUnits(int units)
    {
        // Hardcoded values are coefficients, that helps offset camera more smoothly, changeble(don't supports)
        float offsetIncreasing = Mathf.Sqrt(units) * 1.6f;
        additionalOffset = new Vector3(0, offsetIncreasing, -offsetIncreasing * 1.28f);
    }
}

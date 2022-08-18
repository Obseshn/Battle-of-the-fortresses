using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float _rotationSpeed = 360f;
    private Quaternion rotateDirection;

    private void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {

            _rigidBody.velocity = new Vector3(_joystick.Horizontal * moveSpeed, 0, _joystick.Vertical * moveSpeed); // Move rb by joystick input

            rotateDirection = Quaternion.LookRotation(_rigidBody.velocity); // Calculate rotation

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, _rotationSpeed); // Apply rotation to rb
        }
    }

    
}

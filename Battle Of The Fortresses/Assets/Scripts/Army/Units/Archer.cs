using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : ArmyUnit
{
    [SerializeField] private static float MaxHealth = 1000;
    [SerializeField] private Rigidbody arrow;
    [SerializeField] private Transform fireStartPosition;

    [SerializeField] private float angleInDegrees;

    [SerializeField] private float g = -9.8f;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void DestroyYourself()
    {
        Destroy(gameObject);
    }

    protected override void DoAttack(Transform targetPosition)
    {
        Debug.Log("Archer attacked!");
        Vector3 fromTo = AttackTarget.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        fireStartPosition.localEulerAngles = new Vector3(-angleInDegrees, 0, 0);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleInRadians = angleInDegrees * Mathf.PI / 180f;

        float velocity2 = (g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * (Mathf.Pow(Mathf.Cos(angleInRadians), 2)));
        float velocity = Mathf.Sqrt(Mathf.Abs(velocity2));

        Rigidbody newArrow = Instantiate(arrow, fireStartPosition.position, fireStartPosition.rotation);
        newArrow.velocity = fireStartPosition.forward * velocity;
    }
}

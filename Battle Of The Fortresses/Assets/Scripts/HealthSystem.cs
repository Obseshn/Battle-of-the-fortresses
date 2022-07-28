using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _health = 100f;
    public event Action<GameObject> ObjectHasBeenDestroyedEvent;
    
    private void DestroyYourself()
    {
        // Animations and other stuff
        ObjectHasBeenDestroyedEvent?.Invoke(gameObject);
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        if (damage >= _health)
        {
            DestroyYourself();
        }
        _health -= damage;
    }
}

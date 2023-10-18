using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bound : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health;

        if (other.TryGetComponent(out health))
        {
            health.TakeDamage(health.MaximalValue);
        }
    }
}
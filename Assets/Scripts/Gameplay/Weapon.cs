using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform Owner;
    [SerializeField] protected Vector2 RepulsionOffset = new Vector2(1, 3);
    [SerializeField] private int _damage;
    [SerializeField] private float _repulsionPower;

    protected void AppealDamage(Collider2D collider)
    {
        Health health;

        if (collider.gameObject.TryGetComponent(out health) == true)
        {
            health.TakeDamage(_damage);
        }
    }

    protected void AppealRepulsion(Collider2D collider, float flipMultiplyer = 1)
    {
        Repulsor repulsor;

        if (collider.gameObject.TryGetComponent(out repulsor) == true)
        {
            Vector3 targetPosition = collider.transform.position;
            targetPosition += (Vector3)new Vector2(RepulsionOffset.x * flipMultiplyer, RepulsionOffset.y);

            Vector2 direction = (targetPosition - Owner.transform.position).normalized;
            repulsor.TakeRepulsion(direction * _repulsionPower);
        }
    }
}
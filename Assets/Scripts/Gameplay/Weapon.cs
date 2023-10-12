using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _repulsionPower;
    [SerializeField] private Transform _owner;
    [SerializeField] private Vector2 _repulsionDelta = new Vector2(1, 3);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health;

        if (collision.gameObject.TryGetComponent(out health) == true)
        {
            health.TakeDamage(_damage);
            Repulsor repulsor;

            if (collision.gameObject.TryGetComponent(out repulsor) == true)
            {
                float flipMultiplyer = _owner.transform.right.x > 0 ? 1 : -1;

                Vector3 targetPosition = collision.transform.position;
                targetPosition += (Vector3)new Vector2(_repulsionDelta.x * flipMultiplyer, _repulsionDelta.y);

                Vector2 direction = (targetPosition - _owner.transform.position).normalized;
                repulsor.TakeRepulsion(direction * _repulsionPower);
            }
        }
    }
}
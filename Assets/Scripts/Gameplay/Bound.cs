using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bound : MonoBehaviour
{
    [SerializeField] private PlayerRespawner _respawner;
    [SerializeField] private int _damageToPlayer = 30;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy;

        if (other.TryGetComponent(out enemy))
        {
            Health health = enemy.GetComponent<Health>();
            health.TakeDamage(health.MaximalValue);
        }

        Player player;

        if (other.TryGetComponent(out player))
        {
            Health health = player.GetComponent<Health>();
            health.TakeDamage(_damageToPlayer);
            _respawner.Respawn();
        }
    }
}
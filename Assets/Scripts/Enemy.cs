using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerRespawner _respawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player;

        if (collision.gameObject.TryGetComponent(out player) == true)
            _respawner.Respawn();
    }
}

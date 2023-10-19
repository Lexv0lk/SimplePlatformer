using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderWeapon : Weapon
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = (collision.transform.position - transform.position);
        float flipMultiplyer = direction.x > 0 ? 1 : -1;

        AppealDamage(collision.collider);
        AppealRepulsion(collision.collider, flipMultiplyer);
    }
}
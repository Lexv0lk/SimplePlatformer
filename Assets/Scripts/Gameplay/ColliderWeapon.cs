using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderWeapon : Weapon
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AppealDamage(collision.collider);
        AppealRepulsion(collision.collider);
    }
}
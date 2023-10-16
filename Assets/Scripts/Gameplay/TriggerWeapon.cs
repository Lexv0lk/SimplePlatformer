using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerWeapon : Weapon
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        float flipMultiplyer = Owner.transform.right.x > 0 ? 1 : -1;

        AppealDamage(collider);
        AppealRepulsion(collider, flipMultiplyer);
    }
}
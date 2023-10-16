using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [Header("Автоатака")]
    [SerializeField] private bool _autoAttack = false;
    [SerializeField] private float _range = 1f;
    [SerializeField] private LayerMask _attackable;

    private void FixedUpdate()
    {
        if (_autoAttack == false)
            return;

        Collider2D attackableObject = Physics2D.OverlapCircle(transform.position, _range, _attackable);

        if (attackableObject != null && Vector2.Dot(transform.right, attackableObject.transform.position - transform.position) > 0)
            Attack();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}
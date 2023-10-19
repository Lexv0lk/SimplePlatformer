using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [Header("Автоатака")]
    [SerializeField] private bool _autoAttack = false;
    [SerializeField] private float _range = 1f;
    [SerializeField] private LayerMask _attackable;

    private bool _isReady;

    private void Awake()
    {
        _isReady = true;
    }

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
        if (_isReady)
        {
            _animator.SetTrigger("Attack");
            _isReady = false;
        }
    }

    public void GetReady() => _isReady = true;
}
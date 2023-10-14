using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private string _deadLayer;
    [SerializeField] private string _dyingTrigger;
    [SerializeField] private string _deadTrigger;
    [SerializeField] private TargetMover _mover;
    [SerializeField] private EnemyHealthUI _healthBar;

    private Health _health;
    private Animator _animator;

    public event UnityAction Died;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _health.TakenDamage += OnTakenDamage;
    }

    private void OnDisable()
    {
        _health.TakenDamage -= OnTakenDamage;
    }

    public void Die()
    {
        _animator.SetTrigger(_deadTrigger);
        _healthBar.Vanish();
        Died?.Invoke();
    }

    public void Vanish()
    {
        gameObject.SetActive(false);
    }

    private void OnTakenDamage()
    {
        if (_health.CurrentValue > 0)
            return;

        gameObject.layer = LayerMask.NameToLayer(_deadLayer);
        _animator.SetTrigger(_dyingTrigger);
        _mover.Discard();
    }
}
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private string _deadLayer;
    [SerializeField] private string _dyingTrigger;
    [SerializeField] private string _deadTrigger;
    [SerializeField] private string _seePlayerBoolean;
    [SerializeField] private TargetMover _mover;
    [SerializeField] private EnemyHealthUI _healthBar;
    [SerializeField] private GameObject[] _deactivateOnDeathDependencies;
    [SerializeField] private Animator _animator;
    [SerializeField] private FieldOfView _fov;
    [SerializeField] private CharacterFlipper _flipper;

    private Health _health;

    public event UnityAction Died;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.TakenDamage += OnTakenDamage;
        _fov.FoundPlayer += OnFoundPlayer;
        _fov.LostPlayer += OnLostPlayer;
    }

    private void OnDisable()
    {
        _health.TakenDamage -= OnTakenDamage;
        _fov.FoundPlayer -= OnFoundPlayer;
        _fov.LostPlayer -= OnLostPlayer;
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
        if (_fov.CanSeePlayer == false)
            _flipper.Flip();

        if (_health.CurrentValue > 0)
            return;

        foreach (var gameObject in _deactivateOnDeathDependencies)
            gameObject.SetActive(false);

        gameObject.layer = LayerMask.NameToLayer(_deadLayer);
        _animator.SetTrigger(_dyingTrigger);
        _mover.Discard();
    }

    private void OnFoundPlayer(Player player) => _animator.SetBool(_seePlayerBoolean, true);

    private void OnLostPlayer() => _animator.SetBool(_seePlayerBoolean, false);
}
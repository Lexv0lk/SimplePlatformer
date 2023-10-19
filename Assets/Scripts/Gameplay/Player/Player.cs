using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private string _deadLayer;
    [SerializeField] private string _dieTrigger;
    [SerializeField] private UnityEvent _died;

    private PlayerInput _input;
    private CharacterMover _mover;
    private Attacker _attacker;
    private Repulsor _repulsor;
    private Health _health;
    private Animator _animator;

    private void Awake()
    {
        _input = new PlayerInput();
        _mover = GetComponent<CharacterMover>();
        _attacker = GetComponent<Attacker>();
        _repulsor = GetComponent<Repulsor>();
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();

        _input.Player.Jump.performed += context => _mover.Jump();
        _input.Player.Attack.performed += context => _attacker.Attack();
    }

    private void OnEnable()
    {
        _input.Enable();
        _health.TakenDamage += OnTakenDamage;
    }

    private void OnDisable()
    {
        _input.Disable();
        _health.TakenDamage -= OnTakenDamage;
    }

    private void Update()
    {
        if (_repulsor.CanMove == false)
            return;

        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();
        _mover.Move(direction);
    }

    private void OnTakenDamage()
    {
        if (_health.CurrentValue > 0)
            return;

        gameObject.layer = LayerMask.NameToLayer(_deadLayer);
        _animator.SetTrigger(_dieTrigger);
        _input.Disable();
        _died?.Invoke();
    }
}
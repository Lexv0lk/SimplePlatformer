using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Repulsor))]
public class Player : MonoBehaviour
{
    private PlayerInput _input;
    private CharacterMover _mover;
    private Attacker _attacker;
    private Repulsor _repulsor;

    private void Awake()
    {
        _input = new PlayerInput();
        _mover = GetComponent<CharacterMover>();
        _attacker = GetComponent<Attacker>();
        _repulsor = GetComponent<Repulsor>();

        _input.Player.Jump.performed += context => _mover.Jump();
        _input.Player.Attack.performed += context => _attacker.Attack();
    }

    private void OnEnable()
    {
        _input.Enable();
        _repulsor.Repulsed += OnTookRepulsion;
    }

    private void OnDisable()
    {
        _input.Disable();
        _repulsor.Repulsed -= OnTookRepulsion;
    }

    private void Update()
    {
        if (_repulsor.CanMove == false)
            return;

        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();
        _mover.Move(direction);
    }

    private void OnTookRepulsion()
    {
        //_mover.Move(Vector2.zero);
    }
}
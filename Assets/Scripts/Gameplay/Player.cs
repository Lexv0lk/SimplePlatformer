using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(Attacker))]
public class Player : MonoBehaviour
{
    private PlayerInput _input;
    private CharacterMover _mover;
    private Attacker _attacker;

    private void Awake()
    {
        _input = new PlayerInput();
        _mover = GetComponent<CharacterMover>();
        _attacker = GetComponent<Attacker>();

        _input.Player.Jump.performed += context => _mover.Jump();
        _input.Player.Attack.performed += context => _attacker.Attack();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();
        _mover.Move(direction);
    }
}
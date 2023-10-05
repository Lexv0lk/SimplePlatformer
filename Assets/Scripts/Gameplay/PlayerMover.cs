using UnityEngine;

public class PlayerMover : CharacterMover
{
    private PlayerInput _input;

    protected override void Awake()
    {
        base.Awake();

        _input = new PlayerInput();
        _input.Player.Jump.performed += context => Jump();
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
        Move(direction);
    }
}
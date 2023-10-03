using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerInput _input;

    public event UnityAction<float> ChangedSpeedNormalized;
    public event UnityAction Jumped;
    public event UnityAction<float> ChangedVelocity;

    private void Awake()
    {
        _input = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody2D>();

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

    private void Jump()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce);
            Jumped?.Invoke();
        }
    }

    private void Move(Vector2 direction)
    {
        Vector2 currentVelocity = _rigidbody.velocity;
        currentVelocity.x = direction.x * _moveSpeed;
        _rigidbody.velocity = currentVelocity;
        ChangedSpeedNormalized?.Invoke(Mathf.Abs(direction.x));
        ChangedVelocity?.Invoke(currentVelocity.x);
    }
}
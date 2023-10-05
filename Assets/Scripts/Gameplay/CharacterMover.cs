using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterMover : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody;

    public event UnityAction Jumped;
    public event UnityAction<float> ChangedVelocityX;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Jump()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce);
            Jumped?.Invoke();
        }
    }

    protected void Move(Vector2 direction)
    {
        Vector2 currentVelocity = _rigidbody.velocity;
        currentVelocity.x = direction.x * _moveSpeed;
        _rigidbody.velocity = currentVelocity;
        ChangedVelocityX?.Invoke(currentVelocity.x);
    }
}
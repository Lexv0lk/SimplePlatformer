using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;

    [Header("Locomotion")]
    [SerializeField] private float _maxSpeed = 8;
    [SerializeField] private float _acceleration = 200;
    [SerializeField] private float _maxAccelerationForce = 150;

    [Header("Jumping")]
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveGoal;
    private Vector2 _goalVelocity;

    private bool _isStopped = false;

    public GroundChecker GroundChecker => _groundChecker;

    public event UnityAction Jumped;
    public event UnityAction<float> ChangedVelocityX;

    private void Awake()
    {
        _isStopped = false;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isStopped)
            return;

        Vector2 neededAccel = (_goalVelocity - _rigidbody.velocity) / Time.fixedDeltaTime;
        neededAccel = Vector2.ClampMagnitude(neededAccel, _maxAccelerationForce);
        _rigidbody.AddForce(new Vector2(neededAccel.x * _rigidbody.mass, 0));
    }

    public void Deactivate()
    {
        _rigidbody.velocity = Vector2.zero;
        _isStopped = true;
    }

    public void Activate()
    {
        _isStopped = false;
    }

    public void Jump()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Jumped?.Invoke();
        }
    }

    public void Move(Vector2 direction, float speed)
    {
        _moveGoal = direction;
        Vector2 goalVelocity = _moveGoal * speed;
        _goalVelocity = Vector2.MoveTowards(_goalVelocity, goalVelocity, _acceleration);

        ChangedVelocityX?.Invoke(_goalVelocity.x);
    }

    public void Move(Vector2 direction)
    {
        _moveGoal = direction;
        Vector2 goalVelocity = _moveGoal * _maxSpeed;
        _goalVelocity = Vector2.MoveTowards(_goalVelocity, goalVelocity, _acceleration);

        ChangedVelocityX?.Invoke(_goalVelocity.x);
    }
}
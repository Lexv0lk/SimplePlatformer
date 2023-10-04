using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationConnector : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private string _jumpTrigger;
    [SerializeField] private string _groundedBool;
    [SerializeField] private string _speedXFloat;
    [SerializeField] private string _velocityYFloat;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _groundChecker.GroundLost += OnLostGround;
        _groundChecker.GroundFound += OnGrounded;
        _mover.Jumped += OnJumped;
    }

    private void OnDisable()
    {
        _groundChecker.GroundLost -= OnLostGround;
        _groundChecker.GroundFound -= OnGrounded;
        _mover.Jumped -= OnJumped;
    }

    private void Update()
    {
        _animator.SetFloat(_speedXFloat, Mathf.Abs(_playerRigidbody.velocity.x));
        _animator.SetFloat(_velocityYFloat, _playerRigidbody.velocity.y);
    }

    private void OnJumped()
    {
        _animator.SetTrigger(_jumpTrigger);
    }

    private void OnGrounded()
    {
        _animator.SetBool(_groundedBool, true);
    }

    private void OnLostGround()
    {
        _animator.SetBool(_groundedBool, false);
    }
}

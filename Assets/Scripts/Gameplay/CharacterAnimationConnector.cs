using UnityEngine;

public class CharacterAnimationConnector : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Rigidbody2D _characterRigidbody;
    [SerializeField] private string _jumpTrigger;
    [SerializeField] private string _groundedBool;
    [SerializeField] private string _speedXFloat;
    [SerializeField] private string _velocityYFloat;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator.keepAnimatorStateOnDisable = true;
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
        _animator.SetFloat(_speedXFloat, Mathf.Abs(_characterRigidbody.velocity.x));
        _animator.SetFloat(_velocityYFloat, _characterRigidbody.velocity.y);
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

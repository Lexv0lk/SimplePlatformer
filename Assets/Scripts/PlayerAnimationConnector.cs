using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private string _jumpTrigger;
    [SerializeField] private string _groundedBool;
    [SerializeField] private string _speedFloat;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _groundChecker.GroundLost += OnLostGround;
        _groundChecker.GroundFound += OnGrounded;
        _mover.ChangedSpeedNormalized += OnChangedSpeed;
        _mover.Jumped += OnJumped;
    }

    private void OnDisable()
    {
        _groundChecker.GroundLost -= OnLostGround;
        _groundChecker.GroundFound -= OnGrounded;
        _mover.ChangedSpeedNormalized -= OnChangedSpeed;
        _mover.Jumped -= OnJumped;
    }

    private void OnChangedSpeed(float newValue)
    {
        _animator.SetFloat(_speedFloat, newValue);
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

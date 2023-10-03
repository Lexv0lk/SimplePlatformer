using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce);
    }

    private void Move()
    {

    }
}
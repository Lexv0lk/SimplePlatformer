using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterMover))]
public class CharacterFlipper : MonoBehaviour
{
    private CharacterMover _mover;
    private bool _facingRight = true;

    public event UnityAction Flipped;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
    }

    private void OnEnable()
    {
        _mover.ChangedVelocityX += OnChangedDirection;
    }

    private void OnDisable()
    {
        _mover.ChangedVelocityX -= OnChangedDirection;
    }

    private void OnChangedDirection(float newVelocity)
    {
        if (newVelocity > 0 && _facingRight == false || newVelocity < 0 && _facingRight == true)
            Flip();
    }

    private void Flip()
    {
        transform.Rotate(new Vector3(transform.rotation.x, 180 - transform.rotation.y, transform.rotation.z));
        _facingRight = !_facingRight;
        Flipped?.Invoke();
    }
}
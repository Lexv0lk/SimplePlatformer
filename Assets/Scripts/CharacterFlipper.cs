using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class CharacterFlipper : MonoBehaviour
{
    private CharacterMover _mover;
    private bool _facingRight = true;

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
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        _facingRight = !_facingRight;
    }
}

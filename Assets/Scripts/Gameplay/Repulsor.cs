using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Repulsor : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;

    private Rigidbody2D _rigidbody;

    public bool CanMove { get; private set; } = true;

    public event UnityAction Repulsed;
    public event UnityAction RestartedMoving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeRepulsion(Vector2 force)
    {
        CanMove = false;
        Repulsed?.Invoke();
        StartCoroutine(RestartMovingAbility(0.5f));
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private IEnumerator RestartMovingAbility(float lagTime)
    {
        yield return new WaitForSeconds(lagTime);

        while (CanMove == false)
        {
            CanMove = _groundChecker.IsGrounded;
            yield return null;
        }

        RestartedMoving?.Invoke();
    }
}
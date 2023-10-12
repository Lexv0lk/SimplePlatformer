using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Repulsor : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public event UnityAction Repulsed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeRepulsion(Vector2 force)
    {
        Repulsed?.Invoke();
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}

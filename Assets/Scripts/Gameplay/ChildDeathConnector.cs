using UnityEngine;

public class ChildDeathConnector : MonoBehaviour
{
    [SerializeField] private Enemy _parent;

    public void Vanish()
    {
        _parent.Vanish();
    }

    public void Die()
    {
        _parent.Die();
    }
}
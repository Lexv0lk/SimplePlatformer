using UnityEngine;

public class ChildAnimationConnector : MonoBehaviour
{
    [SerializeField] private Enemy _parent;
    [SerializeField] private Attacker _parentAttacker;

    public void Vanish()
    {
        _parent.Vanish();
    }

    public void Die()
    {
        _parent.Die();
    }

    public void ReadyToAttack()
    {
        _parentAttacker.GetReady();
    }
}
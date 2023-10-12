using UnityEngine;

public class EnemyHealthUI : HealthUI
{
    [SerializeField] private CharacterFlipper _enemyFlipper;

    protected override void OnEnable()
    {
        base.OnEnable();
        _enemyFlipper.Flipped += OnCharacterFlipped;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _enemyFlipper.Flipped -= OnCharacterFlipped;
    }

    private void OnCharacterFlipped()
    {
        transform.Rotate(new Vector3(transform.rotation.x, 180 - transform.rotation.y, transform.rotation.z));
    }
}
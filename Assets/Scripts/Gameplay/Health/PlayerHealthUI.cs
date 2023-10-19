using TMPro;
using UnityEngine;

public class PlayerHealthUI : HealthUI
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private TextMeshProUGUI _valueText;

    protected override void Start()
    {
        base.Start();
        _valueText.text = $"{_playerHealth.CurrentValue}/{_playerHealth.MaximalValue}";
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        HealthSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        HealthSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float newValue)
    {
        _valueText.text = $"{(int)newValue}/{_playerHealth.MaximalValue}";
    }
}
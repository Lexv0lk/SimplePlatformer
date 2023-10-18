using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialText;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void Finish()
    {
        Time.timeScale = 1f;
    }

    public void Count3() => _tutorialText.text = "3";

    public void Count2() => _tutorialText.text = "2";

    public void Count1() => _tutorialText.text = "1";
}

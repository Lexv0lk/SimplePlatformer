using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinisher : MonoBehaviour
{
    [SerializeField] private string _menuScene;

    public void Finish()
    {
        SceneManager.LoadScene(_menuScene, LoadSceneMode.Single);
    }
}
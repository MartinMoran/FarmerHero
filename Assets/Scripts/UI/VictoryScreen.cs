using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameManager.Instance.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    void Start()
    {
        Time.timeScale = 1f;
        Invoke(nameof(LoadNextScene), 2f);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

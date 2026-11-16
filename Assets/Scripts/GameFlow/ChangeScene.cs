using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    void Start()
    {
        GameManager.Instance.ResumeGame();
        Invoke(nameof(LoadNextScene), 2f);
    }

    private void LoadNextScene()
    {
        GameManager.Instance.LoadScene(nextSceneName);
    }
}

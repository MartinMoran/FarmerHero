using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;

        if (pausePanel.activeSelf)
        {
            ResumeGame();
        }
        else if (GameManager.Instance.State == GameState.Playing && Time.timeScale > 0f)
        {
            GameManager.Instance.PauseGame();
            pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.LoadScene("01_Start_Menu");
    }
}

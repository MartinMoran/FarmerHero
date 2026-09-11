using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SurvivalTimer : MonoBehaviour
{
    [SerializeField] private float survivalTime = 180f;
    [SerializeField] private string victorySceneName;
    [SerializeField] private TextMeshProUGUI timerText;

    private float timer = 0f;
    private bool finished = false;

    void Update()
    {
        UpdateTimerUI();

        if (finished) return;

        timer += Time.deltaTime;

        if (timer >= survivalTime)
        {
            finished = true;

            PlayerInteraction interaction = FindAnyObjectByType<PlayerInteraction>();
            if (interaction != null)
                interaction.enabled = false;

            Time.timeScale = 1f;
            SceneManager.LoadScene(victorySceneName);
        }
    }

    private void UpdateTimerUI()
    {
        float remaining = survivalTime - timer;
        if (remaining < 0) remaining = 0;

        int minutes = Mathf.FloorToInt(remaining / 60);
        int seconds = Mathf.FloorToInt(remaining % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
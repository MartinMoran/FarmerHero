using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private string gameOverSceneName;
    [SerializeField] private UnityEngine.UI.Image healthBarFill;

    void Start()
    {
        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateHealthBar();
            Die();
            return;
        }

        UpdateHealthBar();
    }

    private void Die()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

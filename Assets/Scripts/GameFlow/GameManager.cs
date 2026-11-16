using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Punto único de control para pausa, estado de partida y cambio de escena.
/// Se crea automáticamente antes de cargar la primera escena.
/// </summary>
public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<GameState> StateChanged;
    public GameState State { get; private set; } = GameState.Playing;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateAutomatically()
    {
        if (Instance != null) return;
        new GameObject(nameof(GameManager)).AddComponent<GameManager>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        gameObject.AddComponent<PlayerInventory>();
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        SetState(GameState.Paused);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        SetState(GameState.Playing);
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "02_Level 1")
        {
            GetComponent<PlayerInventory>().ResetInventory();
        }

        ResumeGame();
        SceneManager.LoadScene(sceneName);
    }

    public void DeclareVictory(string sceneName)
    {
        Time.timeScale = 1f;
        SetState(GameState.Victory);
        SceneManager.LoadScene(sceneName);
    }

    public void DeclareDefeat(string sceneName)
    {
        Time.timeScale = 1f;
        SetState(GameState.Defeat);
        SceneManager.LoadScene(sceneName);
    }

    private void SetState(GameState newState)
    {
        State = newState;
        StateChanged?.Invoke(State);
    }
}
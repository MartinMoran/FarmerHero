using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void StartGame()
    {
        GameManager.Instance.LoadScene("02_Level 1");
    }
}

using TMPro;
using UnityEngine;

public class StartMessage : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;
        dialoguePanel.SetActive(true);
        dialogueText.text = "Objetivo: Recolecta recursos para crear unidades con el boton C y defender la aldea.\n\nDebes mantenerte vivo por 3 minutos.\n\nPresiona E para comenzar.";
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.E))
        {
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
            gameStarted = true;
        }
    }
}

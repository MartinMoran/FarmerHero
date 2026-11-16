using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private int repairCost = 50;
    [SerializeField] private int repairAmount = 100;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    private bool dialogueActive = false;
    private Building currentBuilding;
    private Animator animator;

    void Start()
    {
        //animator = GetComponent<Animator>();
        inventory = GameManager.Instance.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        // Cerrar diálogo con tecla E.
        if (Input.GetKeyDown(KeyCode.E) && dialogueActive == true)
        {
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
            dialogueActive = false;
            currentBuilding = null;
        }

        // Reparar con tecla R
        if (Input.GetKeyDown(KeyCode.R) && currentBuilding != null)
        {

            if (inventory == null || inventory.SpendResource(ResourceType.Wood, repairCost) == false)
            {
                dialogueText.text = "No tenes suficiente madera para reparar.\n\nPresiona E para continuar.";
                Time.timeScale = 0f;
                return;
            }

            dialogueText.text = "La casa se está reparando...\n\nPresiona E para continuar.";
            currentBuilding.Repair(repairAmount);
            CheckVictory();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            currentBuilding = building;
            dialoguePanel.SetActive(true);
            dialogueActive = true;
            Time.timeScale = 0f;

            if (currentBuilding.IsRepaired())
            {
                dialogueText.text = "Esta casa ya fue reparada.\n\nPresiona E para continuar.";
                currentBuilding = null;
            }
            else
            {
                dialogueText.text = "Presiona R para reparar la casa.";
            }

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!this.enabled) return;          // si está desactivado, no hace nada
        if (other == null) return;         // si el collider ya no existe, no hace nada

        Building building = other.GetComponent<Building>();

        if (building != null && building == currentBuilding)
        {
            currentBuilding = null;

            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);

            dialogueActive = false;
            Time.timeScale = 1f;
        }
    }

    private void CheckVictory()
    {
        Building[] buildings = FindObjectsByType<Building>(FindObjectsSortMode.None);

        foreach (Building building in buildings)
        {
            if (!building.IsRepaired())
            {
                return;
            }
        }

        // DESACTIVAR PlayerInteraction ANTES DE CAMBIAR DE ESCENA
        PlayerInteraction interaction = FindAnyObjectByType<PlayerInteraction>();
        if (interaction != null)
            interaction.enabled = false;

        SceneManager.LoadScene("03_Level 1 Victory");  
    }
}
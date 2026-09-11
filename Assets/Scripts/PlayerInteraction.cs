using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private float harvestCooldown = 0.4f;
    [SerializeField] private int repairCost = 50;
    [SerializeField] private int repairAmount = 100;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    private bool dialogueActive = false;
    private Building currentBuilding;
    private Resource currentResource;
    private Animator animator;
    private float nextHarvestTime = 0f;

    void Start()
    {
        //animator = GetComponent<Animator>();
        inventory = GetComponentInParent<PlayerInventory>();
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

        // Clic izquierdo para recolectar recursos.
        if (Input.GetMouseButtonDown(0) &&
            currentResource != null &&
            Time.time >= nextHarvestTime)
        {
            nextHarvestTime = Time.time + harvestCooldown;

            // Dispara la animación de recolección.
            //animator.SetTrigger("Harvest");
            Resource resource = currentResource;

            // Recolecta una parte del recurso.
            int gatheredAmount = resource.Collect();
            Debug.Log("Recolectaste: " + gatheredAmount + " de " + resource.GetResourceType());

            // Suma lo recolectado al inventario.
            if (inventory != null)
            {
                inventory.AddResource(resource.GetResourceType(), gatheredAmount);
            }

            // Si el recurso se agotó, dejamos de apuntarlo.
            if (resource.IsDepleted())
            {
                currentResource = null;
            }
        }


        // Reparar con tecla R
        if (Input.GetKeyDown(KeyCode.R) && currentBuilding != null)
        {

            if (inventory == null || inventory.wood < repairCost)
            {
                dialogueText.text = "No tenes suficiente madera para reparar.\n\nPresiona E para continuar.";
                Time.timeScale = 0f;
                return;
            }

            dialogueText.text = "La casa se está reparando...\n\nPresiona E para continuar.";

            inventory.SpendWood(repairCost);
            currentBuilding.Repair(repairAmount);
            CheckVictory();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Resource resource = other.GetComponent<Resource>();
        if (resource != null)
        {
            currentResource = resource;
        }

        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            currentBuilding = building;

            if (currentBuilding.IsRepaired())
            {
                dialoguePanel.SetActive(true);
                dialogueActive = true;
                dialogueText.text = "Esta casa ya fue reparada.\n\nPresiona E para continuar.";
                Time.timeScale = 0f;
                currentBuilding = null;
            }
            else
            {
                dialoguePanel.SetActive(true);
                dialogueActive = true;
                dialogueText.text = "Presiona R para reparar la casa.";
                Time.timeScale = 0f;
            }

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!this.enabled) return;          // si está desactivado, no hace nada
        if (other == null) return;         // si el collider ya no existe, no hace nada

        Resource resource = other.GetComponent<Resource>();
        if (resource != null && resource == currentResource)
        {
            currentResource = null;
        }

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
using UnityEngine;

public class PlayerInteraction2 : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private float harvestCooldown = 0.4f;

    [SerializeField] private GameObject pikePrefab;
    [SerializeField] private GameObject archerPrefab;
    [SerializeField] private GameObject swordPrefab;

    [SerializeField] private Transform pikeSpawnPoint;
    [SerializeField] private Transform archerSpawnPoint;
    [SerializeField] private Transform swordSpawnPoint;

    [SerializeField] private int pikeFoodCost = 20;
    [SerializeField] private int archerWoodCost = 20;
    [SerializeField] private int swordGoldCost = 20;

    private Resource currentResource;
    private float nextHarvestTime = 0f;

    void Start()
    {
        inventory = GetComponentInParent<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            currentResource != null &&
            Time.time >= nextHarvestTime)
        {
            nextHarvestTime = Time.time + harvestCooldown;

            Resource resource = currentResource;
            int gatheredAmount = resource.Collect();

            if (inventory != null)
            {
                inventory.AddResource(resource.GetResourceType(), gatheredAmount);
            }

            if (resource.IsDepleted())
            {
                currentResource = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateTroop();
        }
    }

    private void CreateTroop()
    {
        if (inventory == null)
        {
            return;
        }

        if (inventory.food >= pikeFoodCost)
        {
            inventory.SpendFood(pikeFoodCost);
            Instantiate(pikePrefab, pikeSpawnPoint.position, Quaternion.identity);
            inventory.pikeCount++;
            inventory.RefreshUI();
            return;
        }

        if (inventory.wood >= archerWoodCost)
        {
            inventory.SpendWood(archerWoodCost);
            Instantiate(archerPrefab, archerSpawnPoint.position, Quaternion.identity);
            inventory.archerCount++;
            inventory.RefreshUI();
            return;
        }

        if (inventory.gold >= swordGoldCost)
        {
            inventory.SpendGold(swordGoldCost);
            Instantiate(swordPrefab, swordSpawnPoint.position, Quaternion.identity);
            inventory.swordCount++;
            inventory.RefreshUI();
            return;
        }

        Debug.Log("No tenes recursos suficientes para crear tropas.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Resource resource = other.GetComponentInParent<Resource>();
        if (resource != null)
        {
            currentResource = resource;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Resource resource = other.GetComponentInParent<Resource>();
        if (resource != null && resource == currentResource)
        {
            currentResource = null;
        }
    }
}

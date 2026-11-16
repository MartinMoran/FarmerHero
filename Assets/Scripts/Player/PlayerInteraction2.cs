using UnityEngine;

public class PlayerInteraction2 : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;

    [SerializeField] private GameObject pikePrefab;
    [SerializeField] private GameObject archerPrefab;
    [SerializeField] private GameObject swordPrefab;

    [SerializeField] private Transform pikeSpawnPoint;
    [SerializeField] private Transform archerSpawnPoint;
    [SerializeField] private Transform swordSpawnPoint;

    [SerializeField] private int pikeFoodCost = 20;
    [SerializeField] private int archerWoodCost = 20;
    [SerializeField] private int swordGoldCost = 20;

    void Start()
    {
        inventory = GameManager.Instance.GetComponent<PlayerInventory>();
    }

    void Update()
    {
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

        if (inventory.SpendResource(ResourceType.Food, pikeFoodCost))
        {
            Instantiate(pikePrefab, pikeSpawnPoint.position, Quaternion.identity);
            return;
        }

        if (inventory.SpendResource(ResourceType.Wood, archerWoodCost))
        {
            Instantiate(archerPrefab, archerSpawnPoint.position, Quaternion.identity);
            return;
        }

        if (inventory.SpendResource(ResourceType.Gold, swordGoldCost))
        {
            Instantiate(swordPrefab, swordSpawnPoint.position, Quaternion.identity);
            return;
        }

        Debug.Log("No tenes recursos suficientes para crear tropas.");
    }
}

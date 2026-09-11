using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int food = 0;
    public int wood = 0;
    public int gold = 0;
    [SerializeField] private TextMeshProUGUI woodAmount;
    [SerializeField] private TextMeshProUGUI foodAmount;
    [SerializeField] private TextMeshProUGUI goldAmount;

    public int pikeCount = 0;
    public int archerCount = 0;
    public int swordCount = 0;

    [SerializeField] private TextMeshProUGUI pikeCountText;
    [SerializeField] private TextMeshProUGUI archerCountText;
    [SerializeField] private TextMeshProUGUI swordCountText;


    private void Start()
    {
        UpdateUI();
    }

    // Suma la cantidad recibida al recurso correspondiente.
    public void AddResource(ResourceType type, int amount)
    {

        switch (type)
        {
            case ResourceType.Food:
                food += amount;
                UpdateUI();
                break;

            case ResourceType.Wood:
                wood += amount;
                UpdateUI();
                break;

            case ResourceType.Gold:
                gold += amount;
                UpdateUI();
                break;
        }
    }

    private void UpdateUI()
    {
        if (foodAmount != null) foodAmount.text = food.ToString();
        if (woodAmount != null) woodAmount.text = wood.ToString();
        if (goldAmount != null) goldAmount.text = gold.ToString();

        if (pikeCountText != null) pikeCountText.text = pikeCount.ToString();
        if (archerCountText != null) archerCountText.text = archerCount.ToString();
        if (swordCountText != null) swordCountText.text = swordCount.ToString();
    }

    public void RefreshUI()
    {
        UpdateUI();
    }

    public void SpendWood(int amount)
    {
        if (wood >= amount)
        {
            wood -= amount;
            woodAmount.text = wood.ToString();
        }
    }

    public void SpendFood(int amount)
    {
        if (food >= amount)
        {
            food -= amount;
            foodAmount.text = food.ToString();
        }
    }

    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            goldAmount.text = gold.ToString();
        }
    }
}

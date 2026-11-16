using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour, IInventoryObserver
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private TextMeshProUGUI woodAmount;
    [SerializeField] private TextMeshProUGUI foodAmount;
    [SerializeField] private TextMeshProUGUI goldAmount;
    [SerializeField] private TextMeshProUGUI pikeCountText;
    [SerializeField] private TextMeshProUGUI swordCountText;
    [SerializeField] private TextMeshProUGUI archerCountText;

    public void Refresh()
    {
        OnResourceChanged(ResourceType.Wood, inventory.GetResourceAmount(ResourceType.Wood));
        OnResourceChanged(ResourceType.Food, inventory.GetResourceAmount(ResourceType.Food));
        OnResourceChanged(ResourceType.Gold, inventory.GetResourceAmount(ResourceType.Gold));

        OnTroopsChanged();
    }

    public void OnResourceChanged(ResourceType type, int newTotal)
    {
        switch(type)
        {
            case ResourceType.Wood:
                if(woodAmount != null) woodAmount.text = newTotal.ToString();
                break;
            case ResourceType.Food:
                if(foodAmount != null) foodAmount.text = newTotal.ToString();
                break;
            case ResourceType.Gold:
                if(goldAmount != null) goldAmount.text = newTotal.ToString();
                break;
        }
    }

    public void OnTroopsChanged()
    {
        if (inventory == null) return;
        if (pikeCountText != null) pikeCountText.text = inventory.PikeCount.ToString();
        if(swordCountText != null) swordCountText.text = inventory.SwordCount.ToString();
        if(archerCountText != null) archerCountText.text = inventory.ArcherCount.ToString();
    }

    private void OnEnable()
    {
        inventory = GameManager.Instance.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.RegisterObserver(this);
            Refresh();
        }
    }
    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.UnregisterObserver(this);
        }
    }

}   


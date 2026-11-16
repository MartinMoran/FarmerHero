using UnityEngine;

public class Resource : MonoBehaviour
{
    private enum DepletionBehaviour
    {
        Automatic,
        ReplaceSprite,
        DisableGameObject
    }
    [SerializeField] private ResourceType resourceType = ResourceType.Wood;
    [SerializeField] private int amount = 1;
    [SerializeField] private int harvestAmount = 5;
    [SerializeField] private Sprite depletedSprite;
    [SerializeField] private DepletionBehaviour depletionBehaviour = DepletionBehaviour.Automatic;
    private SpriteRenderer spriteRenderer;
    private Collider2D resourceCollider;
    private bool isDepleted = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        resourceCollider = GetComponent<Collider2D>();
    }

    // Devuelve el tipo de recurso, por ejemplo Food, Wood o Gold
    public ResourceType GetResourceType()
    {
        return resourceType;
    }

    // Devuelve cuánta cantidad da este recurso.
    public int GetAmount()
    {
        return amount;
    }

    // Se llama cuando el jugador recolecta este recurso.
    public int Collect()
    {
        if (isDepleted)
        {
            return 0;
        }

        int gatheredAmount = harvestAmount;

        if (gatheredAmount > amount)
        {
            gatheredAmount = amount;
        }

        amount -= gatheredAmount;
        Debug.Log("Queda en el recurso: " + amount);

        if (amount <= 0)
        {
            Deplete();
        }

        return gatheredAmount;

        //Debug.Log("Collected " + amount + " of " + resourceType);
        //Destroy(gameObject);
    }

    private void Deplete()
    {
        isDepleted = true;

        bool shouldDisable = depletionBehaviour == DepletionBehaviour.DisableGameObject ||
            (depletionBehaviour == DepletionBehaviour.Automatic && resourceType != ResourceType.Wood);

        if (shouldDisable)
        {
            gameObject.SetActive(false);
            return;
        }

        // Para los árboles se conserva el objeto y se muestra su sprite agotado.
        if (depletedSprite != null)
        {
            spriteRenderer.sprite = depletedSprite;
        }

    }

    public bool IsDepleted()
    {
        return isDepleted;
    }

}

using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType resourceType = ResourceType.Wood;
    [SerializeField] private int amount = 1;
    [SerializeField] private int harvestAmount = 5;
    [SerializeField] private Sprite depletedSprite;
    
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

        // Cambia la imagen a la del recurso agotado
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

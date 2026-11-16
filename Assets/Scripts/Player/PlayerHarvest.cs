using UnityEngine;

public class PlayerHarvest : MonoBehaviour
{
    [SerializeField] private float harvestCooldown = 0.4f;
    private PlayerInventory inventory;
    private Animator animator;
    private Resource currentResource;
    private float nextHarvestTime;

    void Start()
    {
        inventory = GameManager.Instance.GetComponent<PlayerInventory>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentResource != null 
            && inventory != null && Time.time >= nextHarvestTime)
        {
            nextHarvestTime = Time.time + harvestCooldown;
            Resource resource = currentResource;
            int gatheredAmount = resource.Collect();

            if (gatheredAmount > 0)
            {
                switch (resource.GetResourceType())
                {
                    case ResourceType.Wood:
                        animator.SetTrigger("chop");
                        break;

                    case ResourceType.Gold:
                        animator.SetTrigger("mine");
                        break;
                    case ResourceType.Food:
                        animator.SetTrigger("cut");
                        break;
                }
            }
                
            inventory.AddResource(resource.GetResourceType(), gatheredAmount);

            if (resource.IsDepleted())
            {
                currentResource = null;
            }
        }
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

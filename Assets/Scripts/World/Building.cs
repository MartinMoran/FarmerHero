using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;
    [SerializeField] private int health = 0;
    [SerializeField] private GameObject fireObject;

    void Start()
    {
        if (fireObject != null)
        {
            fireObject.SetActive(health < maxHealth);
        }
    }

    void Update()
    {
    }

    // Función pública para que el script del Enemigo pueda restarle vida desde fuera
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health < 0)
        {
            health = 0;
        }

        Debug.Log(gameObject.name + " recibió daño. Vida restante: " + health);

        if (fireObject != null)
        {
            fireObject.SetActive(health < maxHealth);
        }
    }

    public void Repair(int amount)
    {
        health += amount;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }

        if (fireObject != null)
        {
            fireObject.SetActive(health < maxHealth);
        }
    }

    public bool IsRepaired()
    {
        return health >= maxHealth;
    }
}

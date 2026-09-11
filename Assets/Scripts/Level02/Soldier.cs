using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;
    private float nextAttackTime = 0f;
    private GameObject currentEnemy; // Guarda el enemigo con el que está chocando

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // SI ESTÁ CHOCANDO CON UN ENEMIGO: Se queda quieto y ataca
        if (currentEnemy != null)
        {
            rb.linearVelocity = Vector2.zero;

            if (Time.time >= nextAttackTime)
            {
                Enemy enemyScript = currentEnemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damage);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
        // SI NO ESTÁ TOCANDO A NADIE: Busca al enemigo y camina hacia él
        else
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        GameObject closestEnemy = enemies[0];
        float closestDistance = Vector2.Distance(transform.position, closestEnemy.transform.position);

        for (int i = 1; i < enemies.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, enemies[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemies[i];
            }
        }

        Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    // Función pública por si el enemigo le quiere pegar a este soldado
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(gameObject.name + " (Soldado) recibió daño. Vida: " + health);

        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta si chocó contra el enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentEnemy = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentEnemy = null; // Si se separan, deja de atacar
        }
    }
}

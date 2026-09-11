using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 80;
    [SerializeField] private int damage = 20;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int movHorizontal = 0;
    private Rigidbody2D rb;
    private float nextAttackTime = 0f;
    private GameObject currentTarget; // Guarda el objeto que está tocando

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void Update()
    {
        FlipSprite();
        // SI ESTÁ CHOCANDO CON ALGO: Se queda quieto y ataca (soldado o edificio)
        if (currentTarget != null)
        {
            rb.linearVelocity = Vector2.zero;

            if (Time.time >= nextAttackTime)
            {
                PlayerHealth playerHealth = currentTarget.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
        // SI NO ESTÁ TOCANDO A NADIE: Camina hacia su objetivo automático
        else
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // Si encontró cualquiera de los dos objetivos, camina hacia él
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Si no hay objetivos, se queda quieto
        }
    }

    // Función pública para que los Soldados le peguen a este enemigo
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(gameObject.name + " (Enemigo) recibió daño. Vida: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
            
    }

    private void FlipSprite()
    {
        if (player == null) return;

        // Si el jugador está a la DERECHAdel NPC
        if (player.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false; // NPC mira a la derecha
        }
        // Si el jugador está a la IZQUIERDA del NPC
        else if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true; // NPC mira a la izquierda
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // el enemigo detecta al player
        if (collision.gameObject.CompareTag("Player"))
        {
            currentTarget = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentTarget = null;
        }
    }
}

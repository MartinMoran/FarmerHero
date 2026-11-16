using UnityEngine;

public class Soldier : Fighter
{
    private Rigidbody2D rb;
    private Animator animator;
    private float nextAttackTime = 0f;
    private GameObject currentEnemy; // Guarda el enemigo con el que está chocando
    [SerializeField] private TroopType troopType;
    private PlayerInventory inventory;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        inventory = GameManager.Instance.GetComponent<PlayerInventory>();
        inventory.ChangeTroopCount(troopType, 1); // Aumenta la cantidad de soldados vivos al inicio
    }

    protected override void Die()
    {
        inventory.ChangeTroopCount(troopType, -1); // Disminuye la cantidad de soldados vivos al morir
        base.Die(); // Llama al método Die() de la clase base para manejar la muerte del soldado
    }

    private void Update()
    {
        // SI ESTÁ CHOCANDO CON UN ENEMIGO: Se queda quieto y ataca
        if (currentEnemy != null)
        {
            FaceTarget(currentEnemy.transform); // Orienta al soldado hacia el enemigo
            rb.linearVelocity = Vector2.zero;

            if (Time.time >= nextAttackTime)
            {
                IDamageable target = currentEnemy.GetComponent(typeof(IDamageable)) as IDamageable;
                if (target != null)
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("attack"); // Activa la animación de ataque
                    }

                    target.TakeDamage(Damage);
                    nextAttackTime = Time.time + AttackCooldown;
                }
            }
        }
        // SI NO ESTÁ TOCANDO A NADIE: Busca al enemigo y camina hacia él
        else
        {
            MoveTowardsTarget();
        }
    }

    private void LateUpdate()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", rb.linearVelocity.magnitude > 0.1f);
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

        FaceTarget(closestEnemy.transform); // Orienta al soldado hacia el enemigo más cercano
        Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * MoveSpeed;
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

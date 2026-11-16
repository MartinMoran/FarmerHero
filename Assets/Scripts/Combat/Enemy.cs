using UnityEngine;

public class Enemy : Fighter
{
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //private int movHorizontal = 0;
    private Rigidbody2D rb;
    private Animator animator;
    private float nextAttackTime = 0f;
    private GameObject currentTarget; // Guarda el objeto que está tocando

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
                IDamageable target = currentTarget.GetComponent(typeof(IDamageable)) as IDamageable;
                if (target != null)
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("attack");
                    }
                    target.TakeDamage(Damage);
                    nextAttackTime = Time.time + AttackCooldown;
                }
            }
        }
        // SI NO ESTÁ TOCANDO A NADIE: Camina hacia su objetivo automático
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
        // Si encontró cualquiera de los dos objetivos, camina hacia él
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * MoveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Si no hay objetivos, se queda quieto
        }
    }

    private void FlipSprite()
    {
        GameObject lookTarget = currentTarget != null ? currentTarget : player;

        if (lookTarget != null)
        {
            FaceTarget(lookTarget.transform); // Orienta al enemigo hacia el objetivo elegido.
        }
    }
    // Evalua si la unidad es su objetivo de ataque y lo selecciona.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SelectContactTarget(collision.gameObject);
    }

    // Sigue evaluando si la unidad es su objetivo de ataque mientras esté en contacto y elije otro soldado cercano si muere el anterior.
    private void OnCollisionStay2D(Collision2D collision)
    {
        SelectContactTarget(collision.gameObject);
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentTarget)
        {
            currentTarget = null;
        }
    }

    // Prioriza el ataque a soldados sobre el jugador, pero si no hay soldados, ataca al jugador
    private void SelectContactTarget(GameObject unit)
    {
        if (unit.CompareTag("Soldier"))
        {
            if (currentTarget == null || currentTarget.CompareTag("Player"))
            {
                currentTarget = unit;
            }
        }
        else if (unit.CompareTag("Player") && currentTarget == null)
        {
            currentTarget = unit;
        }
    }


}

using UnityEngine;

public abstract class Fighter : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float moveSpeed = 3f;

    protected int Damage => damage;
    protected float AttackCooldown => attackCooldown;
    protected float MoveSpeed => moveSpeed;
    public int CurrentHealth => health;
    private SpriteRenderer unitSprite;

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || health <= 0) return;
        health = Mathf.Max(0, health - amount);
        Debug.Log($"{gameObject.name} recibió daño. Vida: {health}");
        if (health == 0) Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void Awake()
    {
        unitSprite = GetComponent<SpriteRenderer>();
    }

    protected void FaceTarget(Transform target)
    {
        float distanceX = target.position.x - transform.position.x;

        if (Mathf.Abs(distanceX) < 0.1f)
        {
            return; // No cambiar la dirección si el objetivo está muy cerca en el eje X
        }

        unitSprite.flipX = distanceX < 0f; // Dirige el sprite según la posición del objetivo
    }
}

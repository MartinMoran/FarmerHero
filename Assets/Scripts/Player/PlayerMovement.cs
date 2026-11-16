using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int movHorizontal = 0;
    private int movVertical = 0;
    private Vector2 mov = Vector2.zero;

    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = mov * speed;
    }

    void Update()
    {
        HorizontalMov();
        VerticalMov();
        

        mov = new Vector2(movHorizontal, movVertical).normalized;

        FlipSprite();
        CheckWalk();
    }


    private void HorizontalMov()
    {
        if (Input.GetKey(KeyCode.D))
        {
            movHorizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movHorizontal = -1;
        }
        else
        {
            movHorizontal = 0;
        }
    }

    private void VerticalMov()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movVertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movVertical = -1;
        }
        else
        {
            movVertical = 0;
        }
    }

    // Funcion para activar animacion de caminar/idle dependiendo del movimiento del personaje
    private void CheckWalk()
    {
        animator.SetBool("isWalking", mov.x != 0 || mov.y != 0);
    }

    private void FlipSprite()
    {
        if (movHorizontal != 0)
        {
            spriteRenderer.flipX = movHorizontal < 0;
        }
    }
}

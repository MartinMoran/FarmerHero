using TMPro;
using UnityEngine;

public class Monk : MonoBehaviour
{
    public float interactionRange = 3f;

    [SerializeField] private float speed = 4f;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    private bool dialogueOpen = false;
    private bool hasTalked = false;
    private bool secondMessage = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
 
    void Update()
    {
        FlipSprite();

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        animator.SetBool("isWalking", !hasTalked && distanceToPlayer > interactionRange);

        if (hasTalked == false)
        {
            if (distanceToPlayer > interactionRange)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else
            {
                if (dialogueOpen == false)
                {
                    dialogueOpen = true;
                    dialoguePanel.SetActive(true);
                    dialogueText.text = "La aldea fue atacada!!! Debemos reparar los edificios urgente!!!\n\nPresiona E para continuar...";
                    Time.timeScale = 0f;

                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (secondMessage == false)
                    {
                        dialogueText.text = "Recolecta madera con clic izquierdo en los arboles.\n\nPresiona E para continuar...";
                        secondMessage = true;
                        //hasTalked = true;
                        Time.timeScale = 0f;
                    }
                    else
                    {
                        dialoguePanel.SetActive(false);
                        dialogueOpen = false;
                        hasTalked = true;
                        secondMessage = false;
                        Time.timeScale = 1f;
                        gameObject.SetActive(false);
                    }

                }
            }
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

}

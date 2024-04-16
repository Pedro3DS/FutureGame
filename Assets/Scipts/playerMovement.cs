using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private EnemyBullet enemyBulletScript;

    AudioManager audioManager;

    private Rigidbody2D rb2d;
    private Animator animator;

    private bool isJumping;
    public bool isAlive = true;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (isAlive) // Verifica se o jogador está vivo antes de permitir a movimentação e o pulo
        {
            Movement();
            Jump();
        }
    }

    void Movement()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(movimentoHorizontal * speed, rb2d.velocity.y);
        
        if (movimentoHorizontal < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movimentoHorizontal > 0)
        { 
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (movimentoHorizontal != 0)
        {
            animator.SetBool("Run", true);

        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isJumping)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            animator.SetBool("Jump", false);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Die();
        }
        if(other.gameObject.CompareTag("Enemy")){
            Die();
        }
        if (other.gameObject.CompareTag("Head"))
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("Jump", true);
        }

    }
    void Die()
    {
        isAlive = false;
        animator.SetBool("Hurt", true);
        animator.SetBool("Run", false);
        animator.SetBool("Jump", false);
        rb2d.velocity = Vector2.zero;
        enabled = false;
        audioManager.musicSource.Stop();
    }
    void Hurt()
    {
        animator.SetBool("Duck", true);
    }
}

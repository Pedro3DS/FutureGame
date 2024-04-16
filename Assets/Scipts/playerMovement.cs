using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float cadenciaTiro = 0.5f;
    private float proximoTiro = 0f;

    AudioManager audioManager;

    private Rigidbody2D rb2d;
    private Animator animator;

    private bool isJumping;

    public bool isAlive = true;
    public GameObject bullet;

    public TMP_Text gearsCountText;
    private int gears = 0;
    
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
        gearsCountText.text = $"{gears}";
        if (isAlive)
        {
            Movement();
            Jump();
            Shooting();
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

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= proximoTiro)
        {
            animator.SetBool("Shoot", true);

            proximoTiro = Time.time + cadenciaTiro;

            Vector2 direction = Vector2.right;
            if (gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                direction = Vector2.left;
            }

            Vector3 spawnPosition = transform.position + new Vector3(direction.x, direction.y, 0f);


            GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);


            Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = direction * 10;

            Destroy(bulletRB,3);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gear"))
        {
            audioManager.PlaySFX(audioManager.gear);
            Destroy(other.gameObject);
            gears += 1;
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
    void EndShooting()
    {
        animator.SetBool("Shoot", false);
    }
}

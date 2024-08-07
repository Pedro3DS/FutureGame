using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
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
    public int gears = 0;

    public Canvas gameOverCanvas;

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


    public void Jump(InputAction.CallbackContext inputValue)
    {
        if (inputValue.phase == InputActionPhase.Started && isJumping)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void Shooting(InputAction.CallbackContext inputValue)
    {
        if (Time.time >= proximoTiro && inputValue.phase == InputActionPhase.Started)
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

            Destroy(bulletRB, 3);
        }
    }


    public GameObject explosion;
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
            Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);

        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }


    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && isJumping)
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
        if (other.gameObject.CompareTag("FallLimit"))
        {
            Die();
            rb2d.bodyType = RigidbodyType2D.Static;
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
        gameOverCanvas.gameObject.SetActive(true);
    }
    void EndShooting()
    {
        animator.SetBool("Shoot", false);
    }
}

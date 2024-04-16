using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;


    //public Transform checkGround;
    //public LayerMask layerGround;

    private Rigidbody2D rb2d;
    private Animator animator;

    private bool isJumping;
    //private bool isAttacking = false;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Movement();
        Jump();
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

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("Jump", true);
        }

    }


}

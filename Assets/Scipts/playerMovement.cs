using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 10f;
    //private bool isGround;
    //public Transform checkGround;
    //public LayerMask layerGround;

    private Rigidbody2D rb2d;
    private Animator animator;

    //private bool isAttacking = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

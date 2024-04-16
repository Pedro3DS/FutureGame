using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopController : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private float detectionRange;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    private Vector3 lastKnownPlayerPosition;
    bool right = true;
    bool left = false;
    float speed = 0f;
    bool isShooting = false;
    PlayerMovement playerMovement; // Referência ao script do jogador

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>(); // Obtém o script do jogador
    }

    void Update()
    {
        if (playerMovement.isAlive && Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            StopMoving();
            Shoot();
        }
        else
        {
            ContinueMoving();
        }
        lastKnownPlayerPosition = player.transform.position;
    }

    void StopMoving()
    {
        speed = 0f;
        animator.SetBool("Shooting", true);

    }

    void ContinueMoving()
    {
        speed = 0f;
        animator.SetBool("Shooting", false);
        if (transform.position.x <= maxX && right)
        {
            RightPlataform();
        }
        else if (transform.position.x >= maxX)
        {
            right = false;
            left = true;
        }

        if (left)
        {
            LeftPlataform();
        }
        if (transform.position.x <= minX)
        {
            right = true;
            left = false;
        }
    }

    void RightPlataform()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    void LeftPlataform()
    {
        transform.Translate(-Vector3.right * speed * Time.deltaTime);
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    void Shoot()
    {
        if (!isShooting)
        {
            isShooting = true;
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetTargetPosition(lastKnownPlayerPosition);
            StartCoroutine(ResetShoot());
        }
    }

    IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(1.5f); // Aguarda 1.5 segundos antes de permitir outro tiro
        isShooting = false;
    }
}

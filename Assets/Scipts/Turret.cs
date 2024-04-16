using UnityEngine;

public class Turret : MonoBehaviour
{
    public float distanceToShoot = 10f;
    public Transform player;
    public GameObject enemyShoot;
    public float shootInterval = 2f;
    public int life = 3;
    public GameObject explosion;


    private float timeSinceLastShot = 0f;
    private Vector3 lastKnownPlayerPosition;
    private bool isFacingRight = true;
    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement.isAlive)
        {
            timeSinceLastShot += Time.deltaTime;

            if (player.position.x < transform.position.x && isFacingRight)
            {
                Flip();
            }
            else if (player.position.x > transform.position.x && !isFacingRight)
            {
                Flip();
            }

            lastKnownPlayerPosition = player.position;

            Shooting();
        }
    }

    void Shooting()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= distanceToShoot && timeSinceLastShot >= shootInterval)
        {
            //animator.SetBool("IsShooting", true); TODO: Arrumar animacao do tiro e sincronizar.
            GameObject bullet = Instantiate(enemyShoot, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetTargetPosition(lastKnownPlayerPosition);
            timeSinceLastShot = 0f; // Reinicia o tempo desde o último tiro
        }
        else
        {
            //animator.SetBool("IsShooting", false);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("BulletPlayer"))
        {
            Destroy(other.gameObject);
            life--;
            if (life <= 0)
            {
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

}

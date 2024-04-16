using UnityEngine;

public class Turret : MonoBehaviour
{
    public float distanceToShoot = 10f;
    public Transform player;
    public GameObject enemyShoot;
    public float shootInterval = 2f; 
    private float timeSinceLastShot = 0f; 
    private Vector3 lastKnownPlayerPosition; 
    private bool isFacingRight = true; 

    void Update()
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

    void Shooting(){
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
}

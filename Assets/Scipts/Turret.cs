
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float distanceToShoot = 5f; 
    public Transform player; 
    public Animator animator; 
    public GameObject enemyShoot;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= distanceToShoot)
        {            
            animator.SetBool("IsShooting", true);
            Instantiate(enemyShoot, transform.position, Quaternion.identity);
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }
    }
}

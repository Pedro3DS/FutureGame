using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f; 
    private Vector3 targetPosition; 

   
    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}

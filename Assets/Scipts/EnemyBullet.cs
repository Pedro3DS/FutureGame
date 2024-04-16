using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f; // Velocidade da bala
    private Vector3 targetPosition; // Posição do alvo

    // Método para definir a posição do alvo
    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        // Calcula a direção em que a bala deve se mover
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move a bala na direção calculada
        transform.Translate(direction * speed * Time.deltaTime);

        // Destroi a bala se estiver muito longe do alvo
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referência para o transform do jogador
    public Transform targetSpaceship; // Referência para o transform da nave
    public float smoothSpeed = 0.125f; // Velocidade de suavização do movimento da câmera
    public Vector3 offset; // Distância entre a câmera e o jogador

    void FixedUpdate()
    {
        if (target != null)
        {
            if (target.gameObject.activeSelf) 
            {
                Vector3 desiredPosition = target.position + offset;
                desiredPosition.z = transform.position.z; // Manter a posição Z da câmera fixa
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;

            }
            else
            {
                Vector3 desiredPosition = targetSpaceship.position + offset;
                desiredPosition.z = transform.position.z; // Manter a posição Z da câmera fixa
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}

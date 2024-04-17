using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatingPlataform : MonoBehaviour
{
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    bool up = true;
    bool down = false;
    float speed = 2f;


    void Update()
    {
        if (transform.position.y <= maxY && up)
        {
            RightPlataform();
        }
        else if (transform.position.y >= maxY)
        {
            up = false;
            down = true;
        }

        if (down)
        {
            LeftPlataform();
        }
        if (transform.position.y <= minY)
        {
            up = true;
            down = false;
        }
    }
    void RightPlataform()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    void LeftPlataform()
    {
        transform.Translate(-Vector3.up * speed * Time.deltaTime);
    }
}

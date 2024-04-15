using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    bool right = true;
    bool left = false;
    float speed = 5f;


    void Update()
    {
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
    }
    void LeftPlataform()
    {
        transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 defaultPos;

    [SerializeField] private float fallDelay;
    [SerializeField] private float resplawnTime;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        defaultPos = transform.position;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine("PlataformDrop");
        }
    }

    IEnumerator PlataformDrop()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(resplawnTime);
        Reset();
    }

    private void Reset()
    {
        rb2d.bodyType = RigidbodyType2D.Static;
        transform.position = defaultPos;
    }
}

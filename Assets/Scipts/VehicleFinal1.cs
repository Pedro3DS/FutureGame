using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VehicleFinal1 : MonoBehaviour
{
    public Canvas gameOver;
    private Animator animator;
    

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerMovement>().gears >= 10)
            {
                other.gameObject.SetActive(false);
                animator.SetBool("Success", true);
            }
            else
            {
                other.gameObject.SetActive(false);
                StartCoroutine(FailureFinal());
            }
        }
    }

    IEnumerator FailureFinal()
    {
        animator.SetBool("Failure", true);
        yield return new WaitForSeconds(4f);
        gameOver.gameObject.SetActive(true);
        gameOver.gameObject.GetComponentInChildren<TMP_Text>().text = "Você não coletou todas as engrenagens para a nave funcionar.";
    }
}

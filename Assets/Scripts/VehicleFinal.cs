using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VehicleFinal : MonoBehaviour
{
    public Canvas gameOver;
    public Canvas win;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Player>().gears == 25)
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
        yield return new WaitForSeconds(3f);
        gameOver.gameObject.SetActive(true);
        gameOver.gameObject.GetComponentInChildren<TMP_Text>().text = "Você não coletou todas as engrenagens para a nave funcionar.";
    }

    IEnumerator SuccessFinal()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Creditos");
        yield return new WaitForSeconds(2f);
    }
}

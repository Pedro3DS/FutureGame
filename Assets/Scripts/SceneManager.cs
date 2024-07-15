using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneManager : MonoBehaviour
{
    
    public void RestartFase()
    {  
        UnityEngine.SceneManagement.SceneManager.LoadScene("FirstFase");
    }

    public void Menu(string scena)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scena);
    }

    void Update(){
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "FirstFase" && Input.GetButton("Start")){
            UnityEngine.SceneManagement.SceneManager.LoadScene("FirstFase");
        }
    }

    public void Exit(InputAction.CallbackContext inputValue){
        if( inputValue.phase == InputActionPhase.Started){

            Application.Quit();
        }
    }

    public void RestartFaseControl(InputAction.CallbackContext inputValue){
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FirstFase" && !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isAlive && inputValue.phase == InputActionPhase.Started){

            UnityEngine.SceneManagement.SceneManager.LoadScene("FirstFase");
        }
    }
}

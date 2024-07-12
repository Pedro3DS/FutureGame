using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(Input.GetButton("Start")){
            RestartFase();
        }
    }
}

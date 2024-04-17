using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public string scene;
    

    public void RestartFase()
    {  
        UnityEngine.SceneManagement.SceneManager.LoadScene("FirstFase");
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public TMP_Text endPhraseText;
    List<string> endPhrases = new List<string> {
        "Não foi dessa vez","Um revés momentâneo, mas você vai se recuperar!", "Às vezes até os melhores tropeçam.",
        "Não se preocupe, a próxima rodada está logo ali!", "A jornada para a vitória tem altos e baixos. Esta foi apenas uma curva.",
        "Não deixe uma derrota desanimá-lo", "Sua determinação é mais forte do que qualquer derrota."
    };

    private void Start()
    {
        var rdn = new System.Random();
        endPhraseText.text = endPhrases[rdn.Next(0, endPhrases.Count)];
    }

    public void RestartFase()
    {  
        UnityEngine.SceneManagement.SceneManager.LoadScene("FirstFase");
    }
}

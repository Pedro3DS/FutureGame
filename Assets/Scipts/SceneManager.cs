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
        "N�o foi dessa vez","Um rev�s moment�neo, mas voc� vai se recuperar!", "�s vezes at� os melhores trope�am.",
        "N�o se preocupe, a pr�xima rodada est� logo ali!", "A jornada para a vit�ria tem altos e baixos. Esta foi apenas uma curva.",
        "N�o deixe uma derrota desanim�-lo", "Sua determina��o � mais forte do que qualquer derrota."
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

using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.05f;  // Tempo entre cada letra
    public string fullText;      // Texto completo a ser exibido
    private string currentText = "";

    private TextMeshProUGUI textComponent;

    public AudioSource audioSource;  // Fonte de som
    public AudioClip typingSound;    // Som de digitação

    private int letterCount = 0;     // Contador de letras válidas

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;

            if (i > 0)
            {
                char currentChar = fullText[i - 1];

                if (char.IsLetter(currentChar))  // Só conta letras
                {
                    letterCount++;

                    if (letterCount % 2 == 0)    // A cada 2 letras
                    {
                        if (typingSound != null && audioSource != null)
                        {
                            audioSource.PlayOneShot(typingSound);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(delay);
        }

        // Notifica o fim do texto
        GameObject.Find("Scene_controller").GetComponent<SceneTransitionOnTextEnd>().OnTextFinished();
    }
}

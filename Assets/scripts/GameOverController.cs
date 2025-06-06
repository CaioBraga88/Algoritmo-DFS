using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoTentarNovamente : MonoBehaviour
{
    public void TentarNovamente()
    {
        // Retoma o tempo normal antes de recarregar
        Time.timeScale = 1f;

        string ultimaCena = PlayerPrefs.GetString("UltimaCena");

        if (!string.IsNullOrEmpty(ultimaCena))
        {
            SceneManager.LoadScene(ultimaCena);
        }
        else
        {
            SceneManager.LoadScene("Fase1"); // Substitua com o nome correto da primeira fase
        }
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal"); // Substitua pelo nome da sua cena de menu
    }

    public void SairDoJogo()
    {
        Debug.Log("Fechando o jogo...");
        Application.Quit();
    }
}

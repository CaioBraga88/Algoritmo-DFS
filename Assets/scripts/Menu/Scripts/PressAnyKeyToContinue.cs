using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKeyToContinue : MonoBehaviour
{
    public string nextSceneName = "Mapa1";  // Nome exato da cena

    private bool canProceed = false;

    void Update()
    {
        if (canProceed && Input.anyKeyDown)
        {
            // Carrega a próxima cena
            SceneManager.LoadScene(nextSceneName);
        }
    }

    // Chame este método quando quiser liberar a ação
    public void EnableProceed()
    {
        canProceed = true;
    }
}

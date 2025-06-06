using UnityEngine;
using UnityEngine.SceneManagement;

public class PassagemMapa4 : MonoBehaviour
{
    [SerializeField]
    private string Mapa4;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            CarregarProximaFase();
        }
    }


    private void CarregarProximaFase()
    {
        if (!string.IsNullOrEmpty(Mapa4))
        {
            SceneManager.LoadScene(Mapa4);
            Debug.Log("Carregando a fase: " + Mapa4);
        }
        else
        {
            Debug.LogError("O nome da próxima fase não foi definido no PortalProximaFaseScript do objeto: " + gameObject.name);
        }
    }
}

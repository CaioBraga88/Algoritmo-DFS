using UnityEngine;
using UnityEngine.SceneManagement;

public class PassagemMapa7 : MonoBehaviour
{
    [SerializeField]
    private string Mapa7;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            CarregarProximaFase();
        }
    }


    private void CarregarProximaFase()
    {
        if (!string.IsNullOrEmpty(Mapa7))
        {
            SceneManager.LoadScene(Mapa7);
            Debug.Log("Carregando a fase: " + Mapa7);
        }
        else
        {
            Debug.LogError("O nome da próxima fase não foi definido no PortalProximaFaseScript do objeto: " + gameObject.name);
        }
    }
}

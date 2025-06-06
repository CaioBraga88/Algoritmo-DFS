using UnityEngine;
using UnityEngine.SceneManagement;

public class PassagemMapa8 : MonoBehaviour
{
    [SerializeField]
    private string Mapa8;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            CarregarProximaFase();
        }
    }


    private void CarregarProximaFase()
    {
        if (!string.IsNullOrEmpty(Mapa8))
        {
            SceneManager.LoadScene(Mapa8);
            Debug.Log("Carregando a fase: " + Mapa8);
        }
        else
        {
            Debug.LogError("O nome da próxima fase não foi definido no PortalProximaFaseScript do objeto: " + gameObject.name);
        }
    }
}

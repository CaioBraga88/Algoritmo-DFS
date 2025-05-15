using UnityEngine;
using UnityEngine.SceneManagement;

public class PassagemMapa2 : MonoBehaviour
{
    [SerializeField]
    private string Mapa2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            CarregarProximaFase();
        }
    }


    private void CarregarProximaFase()
    {
        if (!string.IsNullOrEmpty(Mapa2))
        {
            SceneManager.LoadScene(Mapa2);
            Debug.Log("Carregando a fase: " + Mapa2);
        }
        else
        {
            Debug.LogError("O nome da próxima fase não foi definido no PortalProximaFaseScript do objeto: " + gameObject.name);
        }
    }
}

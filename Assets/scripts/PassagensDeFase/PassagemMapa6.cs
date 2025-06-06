using UnityEngine;
using UnityEngine.SceneManagement;

public class PassagemMapa6 : MonoBehaviour
{
    [SerializeField]
    private string Mapa6;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            CarregarProximaFase();
        }
    }


    private void CarregarProximaFase()
    {
        if (!string.IsNullOrEmpty(Mapa6))
        {
            SceneManager.LoadScene(Mapa6);
            Debug.Log("Carregando a fase: " + Mapa6);
        }
        else
        {
            Debug.LogError("O nome da próxima fase não foi definido no PortalProximaFaseScript do objeto: " + gameObject.name);
        }
    }
}

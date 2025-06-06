using UnityEngine;
using UnityEngine.SceneManagement;

public class PassagemMapa5 : MonoBehaviour
{
    [SerializeField]
    private string Mapa5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            CarregarProximaFase();
        }
    }


    private void CarregarProximaFase()
    {
        if (!string.IsNullOrEmpty(Mapa5))
        {
            SceneManager.LoadScene(Mapa5);
            Debug.Log("Carregando a fase: " + Mapa5);
        }
        else
        {
            Debug.LogError("O nome da próxima fase não foi definido no PortalProximaFaseScript do objeto: " + gameObject.name);
        }
    }
}

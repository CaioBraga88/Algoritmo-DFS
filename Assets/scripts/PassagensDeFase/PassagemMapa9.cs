using UnityEngine;
using UnityEngine.SceneManagement;

public class PassagemMapa9 : MonoBehaviour
{
    [SerializeField]
    private string Mapa9;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            CarregarProximaFase();
        }
    }


    private void CarregarProximaFase()
    {
        if (!string.IsNullOrEmpty(Mapa9))
        {
            SceneManager.LoadScene(Mapa9);
            Debug.Log("Carregando a fase: " + Mapa9);
        }
        else
        {
            Debug.LogError("O nome da próxima fase não foi definido no PortalProximaFaseScript do objeto: " + gameObject.name);
        }
    }
}

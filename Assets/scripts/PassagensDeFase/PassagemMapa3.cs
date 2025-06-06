using UnityEngine;
using UnityEngine.SceneManagement;

public class PassagemMapa3 : MonoBehaviour
{
    [SerializeField]
    private string Mapa3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            CarregarProximaFase();
        }
    }


    private void CarregarProximaFase()
    {
        if (!string.IsNullOrEmpty(Mapa3))
        {
            SceneManager.LoadScene(Mapa3);
            Debug.Log("Carregando a fase: " + Mapa3);
        }
        else
        {
            Debug.LogError("O nome da próxima fase não foi definido no PortalProximaFaseScript do objeto: " + gameObject.name);
        }
    }
}

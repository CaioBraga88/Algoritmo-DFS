using UnityEngine;
using UnityEngine.SceneManagement;

public class PassagemFinal : MonoBehaviour
{
    [SerializeField]
    private string Final;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            CarregarProximaFase();
        }
    }


    private void CarregarProximaFase()
    {
        if (!string.IsNullOrEmpty(Final))
        {
            SceneManager.LoadScene(Final);
            Debug.Log("Carregando a fase: " + Final);
        }
        else
        {
            Debug.LogError("O nome da próxima fase não foi definido no PortalProximaFaseScript do objeto: " + gameObject.name);
        }
    }
}

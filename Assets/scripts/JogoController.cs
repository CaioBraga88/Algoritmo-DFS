using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class JogoController : MonoBehaviour
{
    public MovimentoDfs agenteDFS;
    private List<Transform> caminhoDFS;
    private List<Transform> jogadorCaminho = new List<Transform>();

    [Header("Nome da cena de Game Over")]
    public string nomeCenaGameOver = "GameOver"; // Substitua se sua cena tiver outro nome

    void Start()
    {
        // Salva a cena atual como a última jogada
        PlayerPrefs.SetString("UltimaCena", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        if (agenteDFS == null)
        {
            Debug.LogError("Agente DFS não atribuído ao JogoController!");
            enabled = false;
            return;
        }

        caminhoDFS = agenteDFS.GetDFSPath();
        if (caminhoDFS.Count > 0)
        {
            Debug.Log($"Próximo vértice esperado (DFS): {caminhoDFS[0].name}");
        }
    }

    public void JogadorVisitouVertice(Transform verticeVisitado)
    {
        if (caminhoDFS == null) return;

        if (jogadorCaminho.Count == 0)
        {
            if (verticeVisitado == caminhoDFS[0])
            {
                jogadorCaminho.Add(verticeVisitado);
                return;
            }
            else
            {
                IrParaGameOver();
                return;
            }
        }

        if (jogadorCaminho.Count >= 2 && verticeVisitado == jogadorCaminho[jogadorCaminho.Count - 2])
        {
            return;
        }
        else if (jogadorCaminho.Count >= 3 && verticeVisitado == jogadorCaminho[jogadorCaminho.Count - 3])
        {
            return;
        }

        Transform ultimoVerticeJogador = jogadorCaminho.Last();
        int indexUltimoJogadorDFS = caminhoDFS.IndexOf(ultimoVerticeJogador);
        Transform proximoVerticeEsperadoDFS = null;

        for (int i = indexUltimoJogadorDFS + 1; i < caminhoDFS.Count; i++)
        {
            if (!jogadorCaminho.Contains(caminhoDFS[i]))
            {
                proximoVerticeEsperadoDFS = caminhoDFS[i];
                break;
            }
        }

        if (verticeVisitado == proximoVerticeEsperadoDFS)
        {
            jogadorCaminho.Add(verticeVisitado);

            if (verticeVisitado == agenteDFS.targetVertex)
            {
                Debug.Log("Vitória! Caminho correto finalizado.");
                // Aqui você pode chamar a tela de vitória se quiser
            }
        }
        else
        {
            IrParaGameOver();
        }
    }

    void IrParaGameOver()
    {
        SceneManager.LoadScene(nomeCenaGameOver);
    }
}

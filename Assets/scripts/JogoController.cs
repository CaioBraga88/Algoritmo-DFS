using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Para recarregar a cena
using System.Linq; // Para usar Last()

public class JogoController : MonoBehaviour
{
    public MovimentoDfs agenteDFS;
    private List<Transform> caminhoDFS;
    private List<Transform> jogadorCaminho = new List<Transform>(); // Rastreia os nós visitados pelo jogador

    void Start()
    {
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

        // Se for o primeiro nó visitado, deve ser o nó inicial do DFS
        if (jogadorCaminho.Count == 0)
        {
            if (verticeVisitado == caminhoDFS[0])
            {
                jogadorCaminho.Add(verticeVisitado);
                Debug.Log($"Jogador iniciou no nó correto: {verticeVisitado.name}");
                return;
            }
            else
            {
                Debug.LogError("Ordem incorreta! O primeiro nó deve ser o inicial do DFS. Reiniciando.");
                ReiniciarJogo();
                return;
            }
        }

        Transform ultimoVerticeJogador = jogadorCaminho.Last();
        int indexUltimoJogador = caminhoDFS.IndexOf(ultimoVerticeJogador);

        // Encontra o próximo nó não visitado no caminho DFS a partir da posição atual do jogador
        Transform proximoVerticeEsperado = null;
        for (int i = indexUltimoJogador + 1; i < caminhoDFS.Count; i++)
        {
            if (!jogadorCaminho.Contains(caminhoDFS[i]))
            {
                proximoVerticeEsperado = caminhoDFS[i];
                break;
            }
        }

        // Se o jogador voltou para o nó anterior
        if (jogadorCaminho.Count >= 2 && verticeVisitado == jogadorCaminho[jogadorCaminho.Count - 2])
        {
            jogadorCaminho.Add(verticeVisitado);
            Debug.Log($"Jogador voltou para o nó anterior: {verticeVisitado.name}");
            return;
        }

        // Se o jogador visitou o próximo nó esperado
        if (verticeVisitado == proximoVerticeEsperado)
        {
            jogadorCaminho.Add(verticeVisitado);
            Debug.Log($"Ordem correta. Jogador visitou: {verticeVisitado.name}");
            if (verticeVisitado == agenteDFS.targetVertex)
            {
                Debug.Log("Parabéns! Você seguiu o caminho corretamente!");
                // Lógica de vitória
            }
        }
        else
        {
            Debug.LogError($"Ordem incorreta! Você visitou {verticeVisitado.name}, mas esperava-se {(proximoVerticeEsperado != null ? proximoVerticeEsperado.name : "o próximo não visitado")}. Reiniciando.");
            ReiniciarJogo();
        }
    }

    void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
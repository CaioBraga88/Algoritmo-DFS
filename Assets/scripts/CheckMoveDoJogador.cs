using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CheckMoveDoJogador : MonoBehaviour
{
    public MovimentoDfs dfsObject;
    public float nodeReachedThreshold = 2f;
    public float offPathThreshold = 2f;
    private List<Transform> pathToCheck;
    private int currentPathIndex = 0;
    private Transform targetNode;
    private Transform currentNodeJogador = null; // Novo: Rastreia o último nó correto alcançado pelo jogador
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    if (rb2D == null)
    {
        Debug.LogError("Rigidbody2D não encontrado no jogador!");
        enabled = false;
    }

    if (dfsObject == null)
    {
        Debug.LogError("Objeto MovimentoDfs não configurado corretamente");
        enabled = false;
        return;
    }

    pathToCheck = new List<Transform>();
    Debug.Log("CheckMoveDoJogador Start: Tentando inicializar o caminho do DFS...");

    // Tenta obter o correctPath imediatamente no Start
    if (dfsObject.correctPath != null && dfsObject.correctPath.Count > 0)
    {
        Debug.Log("CheckMoveDoJogador Start: Caminho do DFS já disponível.");
        pathToCheck = dfsObject.correctPath;
        if (pathToCheck.Count > 0)
        {
            SetTargetNode(); // Define o primeiro nó como alvo
            enabled = true;
        }
        else
        {
            Debug.LogError("CheckMoveDoJogador Start: Caminho do DFS está vazio!");
            enabled = false;
        }
    }
    else
    {
        Debug.Log("CheckMoveDoJogador Start: Caminho do DFS ainda não gerado.");
        // O Update() lidará com a inicialização quando estiver pronto
    }
    }

    void Update()
    {
        // Verifica se o caminho do DFS já foi inicializado
    if (pathToCheck.Count == 0 && dfsObject != null && dfsObject.correctPath != null && dfsObject.correctPath.Count > 0)
    {
        Debug.Log("CheckMoveDoJogador Update: Caminho do DFS inicializado com " + dfsObject.correctPath.Count + " nós.");
        pathToCheck = dfsObject.correctPath;
        if (pathToCheck.Count > 0 && targetNode == null) // Verifica se targetNode ainda não foi definido
        {
            SetTargetNode();
            enabled = true; // Habilita a lógica de checagem agora
        }
        else if (pathToCheck.Count == 0)
        {
            Debug.LogError("Caminho do DFS está vazio mesmo após a inicialização!");
            enabled = false;
        }
    }
    else if (targetNode != null && rb2D != null)
    {
        float distanceToTarget = Vector2.Distance(transform.position, targetNode.position);
        Debug.Log("CheckMoveDoJogador Update: Distância para o nó " + (currentPathIndex < pathToCheck.Count ? pathToCheck[currentPathIndex].name : "final") + ": " + distanceToTarget + ", currentPathIndex: " + currentPathIndex);

        if (distanceToTarget <= nodeReachedThreshold)
        {
            PlayerReachedNode(targetNode);
        }
        else if (currentPathIndex < pathToCheck.Count && distanceToTarget > (currentPathIndex == 0 ? offPathThreshold * 3 : offPathThreshold))
        {
            if (currentPathIndex > 0 && Vector2.Distance(transform.position, pathToCheck[currentPathIndex - 1].position) > nodeReachedThreshold)
            {
                Debug.Log("Jogador se afastou demais do caminho! Resetando a fase...");
                ResetPhase();
            }
            else if (currentPathIndex == 0 && distanceToTarget > offPathThreshold * 2)
            {
                Debug.Log("Jogador se afastou demais do ponto inicial! Resetando a fase...");
                ResetPhase();
            }
        }
    }
    // Se o caminho ainda não foi inicializado, o Update não faz nada.
    }

    public void PlayerReachedNode(Transform reachedNode)
    {
        Debug.Log("PlayerReachedNode: Jogador tocou em " + reachedNode.name + ", esperado: " + (targetNode != null ? targetNode.name : "null") + ", currentPathIndex: " + currentPathIndex + ", currentNodeJogador: " + (currentNodeJogador != null ? currentNodeJogador.name : "null"));

        if (currentPathIndex < pathToCheck.Count && reachedNode == pathToCheck[currentPathIndex])
        {
            Debug.Log("PlayerReachedNode: Nó correto! " + reachedNode.name);
            currentNodeJogador = reachedNode; // Atualiza o nó atual do jogador
            currentPathIndex++;
            SetTargetNode();
        }
        else
        {
            // Verifica se o jogador já alcançou algum nó correto antes de resetar
            if (currentNodeJogador != null)
            {
                Debug.Log("PlayerReachedNode: Nó incorreto! Resetando...");
                ResetPhase();
            }
            else
            {
                Debug.Log("PlayerReachedNode: Tocou no primeiro nó, aguardando movimento...");
                // Podemos adicionar aqui uma lógica para não resetar imediatamente
                // ou assumir que o primeiro toque no primeiro nó é correto.
                if (currentPathIndex == 0 && reachedNode == pathToCheck[0])
                {
                    Debug.Log("PlayerReachedNode: Primeiro nó tocado, avançando.");
                    currentNodeJogador = reachedNode;
                    currentPathIndex++;
                    SetTargetNode();
                }
                else if (pathToCheck.Count > 0 && reachedNode != pathToCheck[0])
                {
                    Debug.Log("PlayerReachedNode: Tocou em nó errado no início! Resetando...");
                    ResetPhase();
                }
            }
        }
    }

    void SetTargetNode()
    {
        if (currentPathIndex < pathToCheck.Count)
        {
            targetNode = pathToCheck[currentPathIndex];
            Debug.Log("SetTargetNode: Próximo nó esperado: " + targetNode.name + " (Position: " + targetNode.position + "), currentPathIndex: " + currentPathIndex);
        }
        else
        {
            targetNode = null;
            Debug.Log("SetTargetNode: Caminho completo. targetNode é null.");
        }
    }

    void ResetPhase()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Debug.Log("ResetPhase: Fase reiniciada.");
    }
}
using UnityEngine;
using System.Collections.Generic;

public class MovimentoDfs : MonoBehaviour
{
    public Transform startVertex; // O vértice inicial
    public Transform targetVertex; // O vértice final
    public float moveSpeed = 5f; // Velocidade de movimento entre as casas
    public float arrivalThreshold = 0.01f; // Tolerância para considerar que chegou ao nó

    private HashSet<Transform> visitedVertices = new HashSet<Transform>();
    private Stack<Transform> pathStack = new Stack<Transform>();
    private Transform currentTarget; // O nó atual
    private Transform nextTarget; // O próximo nó a se mover
    private bool isMoving = false;
    private bool reachedTarget = false;
    private Rigidbody2D rb;
    private List<Transform> dfsPath = new List<Transform>(); // Lista para armazenar o caminho do DFS

    void Start()
    {
        if (startVertex == null || targetVertex == null)
        {
            Debug.LogError("Vértice inicial ou final não atribuídos no MoverAgente!");
            enabled = false;
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody 2D não encontrado no GameObject com o script MovimentoDfs!");
            enabled = false;
            return;
        }

        // Inicializa o DFS
        pathStack.Push(startVertex);
        visitedVertices.Add(startVertex);
        currentTarget = startVertex;
        dfsPath.Add(startVertex); // Adiciona o vértice inicial ao caminho
        isMoving = true;
        FindNextTarget();
    }

    void FixedUpdate()
    {
        if (isMoving && nextTarget != null && !reachedTarget && rb != null)
        {
            Vector2 targetPosition = nextTarget.position;
            Vector2 currentPosition = rb.position;

            Vector2 moveDirection = (targetPosition - currentPosition).normalized;
            Vector2 newPosition = currentPosition + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if (Vector2.Distance(currentPosition, targetPosition) < arrivalThreshold)
            {
                rb.MovePosition(targetPosition); // Garante a chegada exata
                currentTarget = nextTarget;
                nextTarget = null;
                if (currentTarget == targetVertex)
                {
                    Debug.Log("Chegou ao vértice final (Tabuleiro DFS): " + targetVertex.name);
                    isMoving = false;
                    reachedTarget = true;
                    return;
                }
                else
                {
                    FindNextTarget();
                }
            }
        }
    }

    void FindNextTarget()
    {
        VerticesDfs currentVertexScript = currentTarget.GetComponent<VerticesDfs>();

        if (currentVertexScript != null)
        {
            foreach (Transform neighbor in currentVertexScript.neighbors)
            {
                if (!visitedVertices.Contains(neighbor))
                {
                    visitedVertices.Add(neighbor);
                    pathStack.Push(neighbor);
                    nextTarget = neighbor;
                    dfsPath.Add(nextTarget); // Adiciona o novo alvo ao caminho
                    Debug.Log("Movendo para o vizinho (Tabuleiro DFS): " + neighbor.name);
                    return;
                }
            }
        }
        else
        {
            Debug.LogError("Script VerticesDfs não encontrado no vértice: " + currentTarget.name);
            isMoving = false;
            currentTarget = null;
            return;
        }

        // Se não há vizinhos não visitados, volta para o vértice anterior na pilha (backtracking do DFS)
        if (pathStack.Count > 0)
        {
            nextTarget = pathStack.Pop();
            dfsPath.Add(nextTarget); // Adiciona o alvo de backtracking ao caminho
            Debug.Log("Backtracking para (Tabuleiro DFS): " + nextTarget.name);
        }
        else
        {
            Debug.Log("Todos os vértices acessíveis foram visitados (Tabuleiro DFS).");
            isMoving = false;
            currentTarget = null;
        }
    }

    public List<Transform> GetDFSPath()
    {
        return dfsPath;
    }
}
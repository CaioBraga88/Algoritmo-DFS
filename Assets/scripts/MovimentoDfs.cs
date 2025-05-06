using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MovimentoDfs : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform startNode;
    private Transform currentNode;
    private Stack<Transform> path = new Stack<Transform>();
    private HashSet<Transform> visitedNodes = new HashSet<Transform>();
    private bool isMoving = false;
    private Transform nextNode;
    private Rigidbody2D rb2D;
    [HideInInspector] public List<Transform> correctPath = new List<Transform>();
    public static MovimentoDfs instance;
    public Transform finalNode;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no objeto!");
        }
        if (startNode != null)
        {
            currentNode = startNode;
            path.Push(currentNode);
            visitedNodes.Add(currentNode);
            correctPath.Add(startNode); // Adiciona o nó inicial
            Debug.Log("MovimentoDfs Start: correctPath count após adicionar startNode: " + correctPath.Count);
            FindNextNode();
        }
        else
        {
            Debug.LogError("O Start Node não foi definido no DFSMover!");
        }
    }

    void FixedUpdate()
    {
        if (isMoving && nextNode != null && rb2D != null)
        {
            Vector2 targetPosition = nextNode.position;
            Vector2 currentPosition = rb2D.position;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            rb2D.MovePosition(newPosition);

            // Se o objeto chegou ao nó atual, pare o movimento e encontre o próximo nó
            if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
            {
                isMoving = false;
                currentNode = nextNode;
                FindNextNode();
            }
        }
    }

    void FindNextNode()
    {
        List<Transform> unvisitedNeighbors = GetUnvisitedNeighbors(currentNode);

        if (unvisitedNeighbors.Count > 0)
        {
            nextNode = unvisitedNeighbors.FirstOrDefault();
            if (nextNode != null)
            {
                path.Push(nextNode);
                visitedNodes.Add(nextNode);
                correctPath.Add(nextNode);
                Debug.Log("MovimentoDfs FindNextNode: Adicionado nó " + nextNode.name + ", correctpath count: " + correctPath.Count);
                isMoving = true; // Inicia o movimento
            }
        }
        else
        {
            if (path.Count > 1)
            {
                path.Pop();
                currentNode = path.Peek();
                FindNextNode();
            }
            else
            {
                Debug.Log("Busca DFS completa!");

                // Marca o último nó visitado como final
                if (correctPath.Count > 0)
                {
                    finalNode = correctPath[correctPath.Count - 1];
                    Debug.Log("Último nó visitado (finalNode): " + finalNode.name);
                    
                    // Destaca o vértice final
                    SpriteRenderer sr = finalNode.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.color = Color.green; // Altera a cor do finalNode para verde
                    }
                    else
                    {
                        Debug.LogWarning("finalNode não possui SpriteRenderer para destacar.");
                    }
                }
                else
                {
                    Debug.LogWarning("O caminho correto (correctPath) está vazio!");
                }

                // Para o movimento, garantindo que o objeto não se mova mais
                isMoving = false;
            }
        }
    }

    List<Transform> GetUnvisitedNeighbors(Transform node)
    {
        VerticesDfs vertexScript = node.GetComponent<VerticesDfs>(); // Alteração aqui: usando VerticesDfs
        if (vertexScript != null)
        {
            return vertexScript.neighbors.Where(neighbor => !visitedNodes.Contains(neighbor)).ToList();
        }
        else
        {
            Debug.LogError("O nó " + node.name + " não possui o script VerticesDfs!"); // Altere a mensagem de erro também
            return new List<Transform>();
        }
    }

    public List<Transform> GetCorrectPath()
    {
        return new List<Transform>(correctPath);
    }
}

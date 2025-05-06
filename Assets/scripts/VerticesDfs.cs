using UnityEngine;
using System.Collections.Generic;

public class VerticesDfs : MonoBehaviour
{
    public List<Transform> neighbors;
    public CheckMoveDoJogador pathChecker;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter no nó: " + gameObject.name + ", other tag: " + other.gameObject.tag);
        if (pathChecker != null && other.CompareTag("Player"))
        {
            Debug.Log("VerticesDfs: Jogador tocou no nó: " + transform.name + ", chamando PlayerReachedNode.");
            pathChecker.PlayerReachedNode(transform); // informa o PathChecker que o jogador chegou a este nó
        }
        else if (pathChecker == null)
        {
            Debug.LogError("Path Checker não atribuído no nó: " + gameObject.name);
        }
    }
}
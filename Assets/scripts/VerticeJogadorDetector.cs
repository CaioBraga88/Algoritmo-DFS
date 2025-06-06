using UnityEngine;

public class VerticeJogadorDetector : MonoBehaviour
{
    public JogoController jogoController;

    void OnTriggerEnter2D(Collider2D other)
    {
        movimentoJogador jogador = other.GetComponent<movimentoJogador>();
        if (jogador != null && jogoController != null)
        {
            jogoController.JogadorVisitouVertice(transform);
        }
    }
}
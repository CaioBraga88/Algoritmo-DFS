using UnityEngine;

public class movimentoJogador : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade de movimento do jogador
    private Rigidbody2D rb;
    public Vector2 movimento;

    // Start is called before the first frame update
    void Start()
    {
        //Obtém a referência ao componente Rigidbody 2D do Jogador
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Obtém a entrada dojogador nos nos eixos horizontal e vertical
        float movimentoHorizontal = Input.GetAxisRaw("Horizontal"); // Teclas A/D ou setas esquerda/direita
        float movimentoVertical = Input.GetAxisRaw("Vertical"); // Teclas W/S ou setas cima/baixo

        // Cria um vetor de movimento
        movimento = new Vector2(movimentoHorizontal, movimentoVertical).normalized;
        // .normalized garante que a velocidade diagonal não seja maior
        rb.linearVelocity = movimento * velocidade;
        
    }
}

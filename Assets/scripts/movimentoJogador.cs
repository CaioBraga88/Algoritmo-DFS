using UnityEngine;

public class movimentoJogador : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade de movimento do jogador
    private Rigidbody2D rb;
    public Vector2 movimento;
    private Animator animator;

    // Novos parâmetros para armazenar a última direção válida
    private float lastX;
    private float lastY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Inicializa as últimas direções (ex: virado para baixo)
        lastX = 0f;
        lastY = -1f; // Ou 1f para virado para cima, 0 para lado, etc.
    }

    void FixedUpdate()
    {
        float movimentoHorizontal = Input.GetAxisRaw("Horizontal");
        float movimentoVertical = Input.GetAxisRaw("Vertical");

        movimento = new Vector2(movimentoHorizontal, movimentoVertical).normalized;
        rb.linearVelocity = movimento * velocidade;

        // Envia os valores de x e y para o Animator (para animações de caminhada)


        // ATUALIZAÇÃO CRUCIAL:
        // Atualiza lastX e lastY SOMENTE se houver movimento
        if (movimento.x != 0 || movimento.y != 0)
        {
            animator.SetFloat("x", movimento.x);
            animator.SetFloat("y", movimento.y);

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);

        }
        
    }
}
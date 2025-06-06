using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    public Sprite[] sprites; // Arraste seus sprites aqui no Inspector
    public float intervaloTroca = 0.2f; // Intervalo em segundos entre as trocas de sprite

    private SpriteRenderer spriteRenderer;
    private int indiceAtual = 0;
    private float tempoDecorrido = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[indiceAtual];
        }
        else
        {
            Debug.LogError("Nenhum sprite atribuído ou SpriteRenderer não encontrado!");
            enabled = false; // Desativa o script se houver um erro
        }
    }

    void Update()
    {
        tempoDecorrido += Time.deltaTime;
        if (tempoDecorrido >= intervaloTroca)
        {
            tempoDecorrido -= intervaloTroca;
            indiceAtual = (indiceAtual + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[indiceAtual];
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroFade : MonoBehaviour
{
    public CanvasGroup blackPanel;
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public GameObject pressAnyKeyText;  // Referência ao objeto de texto

    public float fadeDuration = 2f;
    public float holdBlackTime = 2f;

    void Start()
    {
        blackPanel.alpha = 1f;

        // Garante que o vídeo só exibe quando estiver realmente pronto
        videoPlayer.waitForFirstFrame = true;

        // Configura o RawImage para receber o vídeo
        rawImage.texture = videoPlayer.targetTexture;

        // Começa a tocar o vídeo (vai esperar automaticamente o primeiro frame)
        videoPlayer.Play();

        // Desativa o texto inicialmente
        pressAnyKeyText.SetActive(false);

        // Começa a fazer o fade-out após a espera
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // Mantém a tela preta por um tempo antes de iniciar a transição
        yield return new WaitForSeconds(holdBlackTime);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            blackPanel.alpha = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        blackPanel.alpha = 0f;

        // Ativa o texto após o fade e o vídeo começar
        pressAnyKeyText.SetActive(true);

        // Libera a possibilidade de apertar qualquer tecla
        GetComponent<PressAnyKeyToContinue>().EnableProceed();
    }
}

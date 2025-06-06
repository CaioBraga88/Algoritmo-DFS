using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager2 : MonoBehaviour
{
    public string[] validScenes = { "Mapa1", "Mapa2", "Mapa3", "Mapa4", "Mapa5", "Mapa6", "Mapa7", "Mapa8", "Mapa9", "Mapa10" };
    private static MusicManager2 instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.Play();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (System.Array.IndexOf(validScenes, scene.name) == -1)
        {
            Destroy(gameObject); // Destroi quando sair das cenas válidas
        }
    }
}

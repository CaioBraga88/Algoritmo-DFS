using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager1 : MonoBehaviour
{
    public string[] validScenes = { "Menu", "Mapa1" };
    private static MusicManager1 instance;
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

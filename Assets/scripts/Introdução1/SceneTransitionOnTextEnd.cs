using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionOnTextEnd : MonoBehaviour
{
    public string nextSceneName;  
    public GameObject nextIcon;  
    public bool textFinished = false;

    void Start()
    {
        if (nextIcon != null)
        {
            nextIcon.SetActive(false);
        }
    }

    void Update()
    {
        if (textFinished)
        {
            if (nextIcon != null && !nextIcon.activeSelf)
            {
                nextIcon.SetActive(true);
            }

            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    public void OnTextFinished()
    {
        textFinished = true;
    }
}

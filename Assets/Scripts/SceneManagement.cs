using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "credits")
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else Application.Quit();
        }
    }
}
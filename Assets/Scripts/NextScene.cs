using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextScene;
    
    public bool player1should, player2should, bothShouldReach;

    bool player1inside, player2inside;

    private void Start()
    {
        player1inside = player2inside = false;
    }

    private void Update()
    {
        if (bothShouldReach && player1inside && player2inside)
            SceneManager.LoadScene(nextScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            player2inside = true;

            if (player2should)
                SceneManager.LoadScene(nextScene);
        }

        if (collision.gameObject.tag == "Player")
        {
            player1inside = true;

            if (player1should)
                SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            player2inside = false;
        }

        if (collision.gameObject.tag == "Player")
        {
            player1inside = false;
        }
    }
}

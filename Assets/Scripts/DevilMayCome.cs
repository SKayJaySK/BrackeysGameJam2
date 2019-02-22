using UnityEngine;
using UnityEngine.SceneManagement;

public class DevilMayCome : MonoBehaviour
{
    public float moveSpeed = 100f;

    GameObject P1, P2;

    private void Start()
    {
        P1 = GameObject.FindGameObjectWithTag("Player");
        P2 = GameObject.FindGameObjectWithTag("Player2");
    }

    private void Update()
    {
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x > P1.transform.position.x || transform.position.x > P2.transform.position.x)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public bool player1, player2;
    public GameObject P1, P2;

    public Vector3 respawn1pos, respawn2pos;

    private void Start()
    {
        if (player1)
            respawn1pos = P1.transform.position;
        if (player2)
            respawn2pos = P2.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player1 && collision.gameObject.tag == "Player")
        {
            StartCoroutine("Player1");
        }

        if (player2 && collision.gameObject.tag == "Player2")
        {
            StartCoroutine("Player2");
        }
    }

    IEnumerator Player1()
    {
        yield return new WaitForSeconds(0.1f);
        P1.transform.position = respawn1pos;
    }

    IEnumerator Player2()
    {
        yield return new WaitForSeconds(0.1f);
        P2.transform.position = respawn2pos;
    }
}

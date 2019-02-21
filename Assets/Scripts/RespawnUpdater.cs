using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnUpdater : MonoBehaviour
{
    public bool player1, player2;

    Respawn rp;

    private void Start()
    {
        rp = transform.GetComponentInParent<Respawn>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player1 && !player2 && collision.gameObject.tag == "Player")
        {
            rp.respawn1pos = transform.position + new Vector3(0, 1, 0);
            gameObject.SetActive(false);
        }

        if (player2 && !player1 && collision.gameObject.tag == "Player2")
        {
            rp.respawn2pos = transform.position + new Vector3(0, 1, 0);
            gameObject.SetActive(false);
        }

        if (player2 && player1 && (collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player1"))
        {
            rp.respawn2pos = transform.position + new Vector3(0, 1, 0);
            rp.respawn1pos = transform.position + new Vector3(-1, 1, 0);
            gameObject.SetActive(false);
        }
    }
}

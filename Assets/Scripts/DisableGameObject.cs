using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    public int player;

    public GameObject disableThis;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == 2 && collision.gameObject.tag == "Player2")
        {
            disableThis.SetActive(false);
        }

        if (player == 1 && collision.gameObject.tag == "Player")
        {
            disableThis.SetActive(false);
        }
    }
}
